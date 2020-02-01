using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Boid : MonoBehaviour
{
	static public List<Boid> boids;   //此静态List用于存放所有Boid实例

	public Vector3 velocity;          //当前帧速度
	public Vector3 newVelocity;       //下一帧速度
	public Vector3 newPosition;       //下一帧位置

	public List<Boid> neighbors;      //附近的所有Boid
	public List<Boid> collisionRisks; //距离过近的所有Boid
	public Boid closest;              //最近的Boid

	private float MaxSpeed;
	private Sprite sp;
	private Vector3 tp;
	private Quaternion tr;

	void Awake()
	{
		MaxSpeed = BoidSpawner.S.maxVelocity;
		if (boids == null) boids = new List<Boid>();//如果List变boids未定义则对其进行定义
		boids.Add(this);//向boids List中添加Boid

		//为当前Boid实例提供随机的位置和速度
		Vector3 randPos = Random.insideUnitSphere * BoidSpawner.S.spawnRadius;
		randPos.z = 0;
		this.transform.position = randPos;
		velocity = Random.onUnitSphere * BoidSpawner.S.spawnVelocity;

		//初始化两个List
		neighbors = new List<Boid>();
		collisionRisks = new List<Boid>();

		//让this.transform成为Boid游戏对象的子对象
		var parentBoid = GameObject.Find("Boids");
		if(!parentBoid)
			parentBoid = new GameObject("Boids");
		this.transform.parent = GameObject.Find("Boids").transform;

		//给Boid对象一个随机颜色但不要过暗
		sp = Resources.Load("Idle", typeof(Sprite)) as Sprite;
		Color randColor = Color.black;
		while (randColor.r + randColor.g + randColor.b < 1.0f)
		{
			randColor = new Color(Random.value, Random.value, Random.value);
		}
		SpriteRenderer[] rends = gameObject.GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer sr in rends)
		{
			sr.sprite = sp;
			sr.material.color = randColor;
		}
	}

	void Update()
	{
		List<Boid> neighbors = GetNeighbors(this);//获取附近所有Boids

		//初始化新位置和新速度
		newVelocity = velocity;
		newPosition = this.transform.position;

		//速度匹配：
		Vector3 neighborVel = GetAverageVelocity(neighbors);//使当前Boid速度接近其临近Boid对象的平均速度
		newVelocity += neighborVel * BoidSpawner.S.velocityMatchingAmt;//使用BoidSpawner.S中设置的字段

		//凝聚向心性：向临近Boid对象的中心移动
		Vector3 neighborCenterOffset = GetAveragePosition(neighbors) - this.transform.position;
		newVelocity += neighborCenterOffset * BoidSpawner.S.flockCenteringAmt;

		//排斥性：避免撞到临近的Boid
		Vector3 dist;
		if (collisionRisks.Count > 0)
		{
			Vector3 collisionAveragePos = GetAveragePosition(collisionRisks);
			dist = collisionAveragePos - this.transform.position;
			newVelocity += dist * BoidSpawner.S.collisionAvoidanceAmt;
		}

		//跟随鼠标光标：不论距离多远都向鼠标光标移动,过近则快速离开
		dist = BoidSpawner.S.mousePos - this.transform.position;
		if (dist.magnitude > BoidSpawner.S.mouseAvoidanceDist)
		{
			MaxSpeed = BoidSpawner.S.maxVelocity;
			newVelocity += dist * BoidSpawner.S.mouseAttractionAmt;
		}
		else MaxSpeed = BoidSpawner.S.maxVelocityNear;
	}

	void LateUpdate()
	{//放在LateUpdate中是为了确保全算完了再改

		//基于计算出的新速度修改当前速度(线性插值，p01=(1-u)*p0+u*p1)
		velocity = (1 - BoidSpawner.S.velocityLerpAmt) * velocity + BoidSpawner.S.velocityLerpAmt * newVelocity;

		//确保速度值在上下限之内
		if (velocity.magnitude > MaxSpeed) velocity = velocity.normalized * MaxSpeed;
		if (velocity.magnitude < BoidSpawner.S.minVelocity) velocity = velocity.normalized * BoidSpawner.S.minVelocity;

		//确定新位置
		newPosition = this.transform.position + velocity * Time.deltaTime;
		//将所有对象限制在XY平面上
//		newPosition.z = 0.1f * newPosition.y;
		//从原有位置看向新位置，确定Boid的朝向
		//		this.transform.LookAt(newPosition);
		if (newPosition.x < transform.position.x) GetComponent<SpriteRenderer>().flipX = false;
		else GetComponent<SpriteRenderer>().flipX = true;
		//真正移动到新位置
		this.transform.position = newPosition;
	}

	public List<Boid> GetNeighbors(Boid boi)
	{//boi指BoidOfInterest;该函数寻找boi的附近对象（距离足够近）
		float closestDist = float.MaxValue;
		Vector3 delta;
		float dist;
		neighbors.Clear();
		collisionRisks.Clear();
        //防GetAverageVelocity内丢失
        closest = boids[0];

        foreach (Boid b in boids)
		{
			if (b == boi) continue;
			delta = b.transform.position - boi.transform.position;
			dist = delta.magnitude;
			if (dist < closestDist) { closestDist = dist; closest = b; }
			if (dist < BoidSpawner.S.nearDist) neighbors.Add(b);
			if (dist < BoidSpawner.S.collisionDist) collisionRisks.Add(b);
		}
		if (neighbors.Count == 0) neighbors.Add(closest);
		return neighbors;
	}
	public Vector3 GetAveragePosition(List<Boid> someBoids)
	{//获取一个Boid List中所有Boid的平均位置
		Vector3 sum = Vector3.zero;
		if (someBoids != null)
		{
			foreach (Boid b in someBoids) sum += b.transform.position;
			Vector3 center = sum / someBoids.Count;
			return center;
		}
		else return new Vector3(0, 0, 0);
	}
	public Vector3 GetAverageVelocity(List<Boid> someBoids)
	{//获取一个Boid List中所有Boid的平均速度
		Vector3 sum = Vector3.zero;
		foreach (Boid b in someBoids) sum += b.velocity;
		Vector3 avg = sum / someBoids.Count;
		return avg;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			if (other.gameObject.layer.ToString() == "Tentacle") Destroy(other.gameObject);
			tp = new Vector3(transform.position.x, transform.position.y, 0);
			tr = new Quaternion(0, 0, 0, 0);
			Instantiate(BoidSpawner.S.Substitute, tp, tr);
			boids.Remove(this);
			Destroy(gameObject);
		}
	}
}

