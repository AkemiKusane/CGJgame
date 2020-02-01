using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			AudioCenter.Instance.Play("Jiuren");
			Instantiate(BoidSpawner.S.boidPrefab, other.transform.position, new Quaternion(0, 0, 0, 0));
			Destroy(gameObject);
		}
	}
}
