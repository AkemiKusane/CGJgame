using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayGo : MonoBehaviour
{
    public float delay = 9f;
    public bool interactReady;
    public string goingLevel;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        interactReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!interactReady) return;
        if (string.IsNullOrEmpty(goingLevel)) return;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(goingLevel);
        }
    }
}
