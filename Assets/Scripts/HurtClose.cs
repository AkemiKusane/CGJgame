using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtClose : MonoBehaviour
{
	public float waitSec = 1.5f;
	private Collider[] father;

	// Start is called before the first frame update
	void Start()
    {
		father = GetComponentsInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown("space")) StartCoroutine("Shift");
	}

	IEnumerator Shift() {
		foreach (var child in father) child.enabled=false;
		yield return new WaitForSeconds(waitSec);
		foreach (var child in father) child.enabled=true;
		yield return 0;
	}
}
