using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour {
	static public BoidSpawner S;

	//以下字段调节全体Boid对象的行为
	public int numBoids = 100;
	public GameObject boidPrefab, Substitute;
	public float spawnRadius = 100f;
	public float spawnVelocity = 10f;
	public float minVelocity = 0f;
	public float maxVelocity = 30f;
	public float maxVelocityNear = 0.2f;
	public float nearDist = 30f;
	public float collisionDist = 5f;
	public float velocityMatchingAmt = 0.01f;
	public float flockCenteringAmt = 0.15f;
	public float collisionAvoidanceAmt = -0.5f;
	public float mouseAttractionAmt = 0.01f;
	public float mouseAvoidanceAmt = 0.75f;
	public float mouseAvoidanceDist = 15f;
	public float velocityLerpAmt = 0.25f;

	public bool ________________;

	public Vector3 mousePos;

	private GameObject heroGO;

	// Use this for initialization
	void Start () {
		S = this;
		for (int i = 0; i < numBoids; i++) Instantiate(boidPrefab);
		heroGO = GameObject.Find("Hero");
	}
	void LateUpdate()
	{		
		mousePos = heroGO.transform.position;
//		mousePos = this.GetComponent<Camera>().ScreenToWorldPoint(heroTP);
	}
}
