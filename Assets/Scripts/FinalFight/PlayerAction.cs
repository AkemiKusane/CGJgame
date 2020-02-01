using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameObject[] vfx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(IEPlay());
        }
    }
    private void SpawnVFX() {
        int index = Random.Range(0, vfx.Length);
        GameObject pick = vfx[index];
        Vector3 pos = new Vector3(Random.Range(-4,4),Random.Range(-4,4));
        GameObject nb = Instantiate(pick, transform);
        nb.transform.localPosition = pos;
        nb.transform.rotation = Quaternion.Euler(new Vector3(0, 90, -90));

    }
    IEnumerator IEPlay() {
        SpawnVFX();
        yield return new WaitForSeconds(0.1f);
        SpawnVFX();
        //yield return new WaitForSeconds(0.1f);
        //SpawnVFX();
    }
}
