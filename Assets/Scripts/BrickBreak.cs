using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBreak : MonoBehaviour
{
	public int life = 3;
	private int count;
	private Collider Collider;
	private SpriteRenderer sr;
	private string bName;
    // Start is called before the first frame update
    void Start()
    {
		count = 0;
		Collider = GetComponent<Collider>();
		sr = GetComponent<SpriteRenderer>();
		draw(0);
    }

    // Update is called once per frame
    void Update()
    {
		if (count >= life) Collider.enabled = false;
    }
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Player"))
		{
			count++;
			draw(count);
		}		
	}
	private void draw(int type) {
		switch (type) {
			case 0:bName = "StoneW";break;
			case 1:bName = "HalfSP";AudioCenter.Instance.Play("Qiang1"); break;
			case 2:bName = "TotalS";break;
			default: {
					AudioCenter.Instance.Play("QiangLie");
                sr.sprite = null;

                for (int i = 0; i < transform.childCount; i++) {
                    Destroy(transform.GetChild(i).gameObject);
                }
                return; 
            }

		}
		Sprite spb = Resources.Load(bName, typeof(Sprite)) as Sprite;
		sr.sprite = spb;
    }
}
