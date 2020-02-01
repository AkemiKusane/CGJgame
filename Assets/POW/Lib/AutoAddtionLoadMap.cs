using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoAddtionLoadMap : MonoBehaviour
{
    public string levelName;
    // Start is called before the first frame update
    void Start()
    {
        if (string.IsNullOrEmpty(levelName)) return;
        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
    }

}
