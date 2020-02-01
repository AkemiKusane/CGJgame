using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStory : MonoBehaviour
{
    public bool canGoNext;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(750f / 60f);
        canGoNext = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canGoNext) return;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        }
    }
}
