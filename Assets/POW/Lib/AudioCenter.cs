using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCenter : Sirenix.OdinInspector.SerializedMonoBehaviour {
    public AudioSource[] Wrong;
    public void PlayWrong() {
        foreach (var item in Wrong) {
            if (item.isPlaying) {
                Debug.Log("播放中");
                continue;
            }
            item.Play();
        }
    }
    public Dictionary<string, AudioSource[]> audioObjects;
    private Dictionary<string, int> audioIndex= new Dictionary<string, int>();

    public void Play(string audioname) {
        if (!audioIndex.ContainsKey(audioname)) return;
        int index = audioIndex[audioname];
        audioObjects[audioname][index].Play() ;
        audioIndex[audioname]++;

        if (audioIndex[audioname] >= audioObjects[audioname].Length) {
            audioIndex[audioname] = 0;
        }
    }




    private static AudioCenter instance;
    public static AudioCenter Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<AudioCenter>();
                if (instance == null) {
                    GameObject obj = new GameObject();
                    obj.name = typeof(AudioCenter).Name;
                    instance = obj.AddComponent<AudioCenter>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake() {
        if (instance == null) {
            instance = this as AudioCenter;
            Init();
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    protected virtual void Init() {
        foreach (var item in audioObjects) {
            if (!audioIndex.ContainsKey(item.Key)) {
                audioIndex.Add(item.Key, 0);
            }
        }
    }
}
