using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthControl : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth = 50;
    [SerializeField] GameObject baitiao;
	bool flag;
	Vector3 len;

    // Update is called once per frame
    void Update()
    {
        baitiao.transform.localScale = new Vector2(currentHealth / maxHealth, 1);
		if (flag == true)
		{
			//var aa = baitiao.transform.localScale;
			//baitiao.transform.localScale = new Vector2()
		}
    }
    public void TakeDamage(float dmg)
    {
        currentHealth -=dmg;
        currentHealth = Mathf.Clamp(currentHealth,0,maxHealth);
    }
    public void AddHealth(float add)
    {
        currentHealth +=add;
        currentHealth = Mathf.Clamp(currentHealth,0,maxHealth);
    }

}
