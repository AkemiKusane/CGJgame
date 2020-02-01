using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroBehavior : MonoBehaviour
{
	public Vector3 velocity;          //当前帧速度
	public Vector3 newVelocity;       //下一帧速度
	public Vector3 newPosition;       //下一帧位置
	public float SpeedAmt=10;
	public float JamAmt = 0.5f;
	public float RunRange = 5;
	public float CSpeed = 5;

	private GameObject kidrun;
	private float xInput, yInput;
	private string fname;
	private Color crw;
	public GameObject face,white;

	// Start is called before the first frame update
	void Start()
	{
		kidrun=GameObject.Find("run");
	}

	// Update is called once per frame
	void Update()
	{
		newVelocity = velocity;
		newPosition = this.transform.position;		
		xInput = Input.GetAxis("Horizontal");
		yInput = Input.GetAxis("Vertical");
		if (xInput != 0) newVelocity += new Vector3(SpeedAmt * xInput, 0, 0);
		else newVelocity -= new Vector3(JamAmt * newVelocity.x, 0, 0);
		if (yInput != 0) newVelocity += new Vector3(0, SpeedAmt * yInput, 0);
		else newVelocity -= new Vector3(0,JamAmt * newVelocity.y, 0);
//		if (Input.GetKeyDown("space")) newVelocity -= new Vector3(0, 0, SpeedAmt);
//		if (Input.GetKeyUp("space")) newVelocity += new Vector3(0, 0, 2*SpeedAmt);
	}
	void LateUpdate()
	{//放在LateUpdate中是为了确保全算完了再改
		//基于计算出的新速度修改当前速度(线性插值，p01=(1-u)*p0+u*p1)
		velocity = (1 - BoidSpawner.S.velocityLerpAmt) * velocity + BoidSpawner.S.velocityLerpAmt * newVelocity;
		if (velocity.magnitude < RunRange) kidrun.SetActive(false);
		else kidrun.SetActive(true);

		//确保速度值在上下限之内
		if (velocity.magnitude > BoidSpawner.S.maxVelocity) velocity = velocity.normalized * BoidSpawner.S.maxVelocity;
		if (velocity.magnitude < BoidSpawner.S.minVelocity) velocity = velocity.normalized * BoidSpawner.S.minVelocity;

		//确定新位置
		newPosition = this.transform.position + velocity * Time.deltaTime;
		//将所有对象限制在XY平面上
//		newPosition.z = 0.1f*newPosition.y;
		//从原有位置看向新位置，确定Boid的朝向
		//		this.transform.LookAt(newPosition);
		if (newPosition.x < transform.position.x)
			for(int i=0;i<2;i++)GetComponentsInChildren<SpriteRenderer>()[i].flipX = false;
		else
			for(int i=0;i<2;i++)GetComponentsInChildren<SpriteRenderer>()[i].flipX = true;
		//真正移动到新位置
		this.transform.position = newPosition;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy")) { Pokerface(1);
			FindObjectOfType<LevelController>().GetComponent<LevelController>().DealDamageToHero();
			FindObjectOfType<LevelController>().GetComponent<LevelController>().AddHealthToEnemy();
			if (other.gameObject.layer.ToString() == "Tantacle") AudioCenter.Instance.Play("TentacleHit");

		}
		else if (other.CompareTag("Brick")) { Pokerface(2);}
		else if (other.CompareTag("Friend")) { Pokerface(3);
			FindObjectOfType<LevelController>().GetComponent<LevelController>().AddHealthToHero();
			FindObjectOfType<LevelController>().GetComponent<LevelController>().DealDamageToEnemy();
		}
		else if (other.CompareTag("Finish")) { Finish(); }
	}

	private void OnTriggerExit(Collider other)
	{
		Pokerface(0);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.relativeVelocity.magnitude > BoidSpawner.S.maxVelocity / 2) velocity = velocity.normalized;
	}

	private void Pokerface(int type) {
		switch (type) {
			case 0:fname = "cuttie";break;
			case 1:fname = "insane";break;
			case 2:fname = "hentai";break;
			case 3:fname = "hurray";break;
		}
		SpriteRenderer srf = face.GetComponent<SpriteRenderer>();
		Sprite spf = Resources.Load(fname,typeof(Sprite))as Sprite;
		
		srf.sprite = spf;
	}
	private void Finish() {
		SpriteRenderer srw = white.GetComponent<SpriteRenderer>();
		crw = srw.material.color;
		StartCoroutine(MoveToColor(new Color(255, 255, 255, 255)));
		SceneManager.LoadScene("FinalFight");
	}
	IEnumerator MoveToColor(Color target)
	{
		while (crw!= target)
		{
			crw = Vector4.MoveTowards(crw, target, CSpeed * Time.deltaTime);
			yield return 0;
		}
	}
}