using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubstituteBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		Destroy(transform.parent.gameObject, 0.499f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
