using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyJump : MonoBehaviour
{
	public float waitSec=1.0f;
	public float jumpDist = 0.5f;
	private SpriteRenderer sr;
	private Sprite[] sp;
	// Start is called before the first frame update
	void Start()
    {
		sr = this.GetComponent<SpriteRenderer>();
		sp=new Sprite[3];
		sp[0] = Resources.Load("Counter", typeof(Sprite)) as Sprite;
		sp[1] = Resources.Load("Jump", typeof(Sprite)) as Sprite;
		sp[2]= Resources.Load("Idle", typeof(Sprite)) as Sprite;
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown("space")) StartCoroutine("Shift");
    }

	IEnumerator Shift() {
		AudioCenter.Instance.Play("Jump");
		sr.sprite = sp[0];
		transform.position += new Vector3(0, jumpDist, 0);
		yield return new WaitForSeconds(waitSec/2);
		sr.sprite = sp[1];
		yield return new WaitForSeconds(waitSec/2);
		sr.sprite = sp[2];
		transform.position -= new Vector3(0, jumpDist, 0);
		yield return 0;
	}
}
