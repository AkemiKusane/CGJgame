using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTentacle : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Boid>())
        {
            Destroy(other.gameObject);
        }
    }
    public void StandardPosition()
    {
        transform.position = FindObjectOfType<EnemyController>().transform.position;
    }

    void ExistenceIsPain() {
        Destroy(transform.parent.gameObject);
    }
}
