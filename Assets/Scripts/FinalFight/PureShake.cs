using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PureShake : MonoBehaviour {
    public float nowRadius;
    public float warmupDuration = 1;
    public float oraDuration = 0.2f;
    public float startAngle = 70;
    public float scaleAngle = 220;
    public float distance = 1;
    public bool oraStart = false;

    private int roundCount = 0;
    private Vector3 originPos;
    private float timecount = 0;

    // Start is called before the first frame update
    IEnumerator Start() {
        originPos = transform.position;
        oraStart = true;
        yield return null;
    }

    // Update is called once per frame
    void Update() {
        if (!oraStart) return;
        timecount -= Time.deltaTime;
        if (timecount <= 0) {
            nowRadius = Random.Range(0, Mathf.PI * 2);
            timecount = oraDuration;
        }
        UpdatePos();
    }

    private void UpdatePos() {
        float totalTime;
        totalTime = oraDuration;
        float schedule = 1f - timecount / totalTime;
        float t = 1;
        t = (Mathf.Sin((startAngle + schedule * scaleAngle) / 180 * Mathf.PI) + 1) / 2;
        transform.position = Vector3.Lerp(originPos, originPos + new Vector3(Mathf.Cos(nowRadius) * distance, Mathf.Sin(nowRadius) * distance), t);
    }
}
