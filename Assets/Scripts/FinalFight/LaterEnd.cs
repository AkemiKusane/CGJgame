using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaterEnd : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(14f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GoodEnd");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
