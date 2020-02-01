using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLine : MonoBehaviour
{
    public float timeinterval;
    public GameObject obj;
    public float length = 0.5f;
    private float timecount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timecount -= Time.deltaTime;
        if (timecount <= 0) {
            obj.transform.localPosition = new Vector3(Random.Range(-length, length), Random.Range(-length, length), 5);
            timecount = timeinterval;
        }
    }
}
