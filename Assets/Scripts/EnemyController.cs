
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField][Range(0.01f,0.2f)] float moveSpeed = 0.1f;
    [SerializeField][Range(-7f,-2f)] float leftBorder =-4.5f;
    [SerializeField][Range(7f,2f)] float rightBorder =4.5f;
    [SerializeField][Tooltip("下方弹出的手")] GameObject[] popUpTentacles; 
    [SerializeField][Tooltip("钻出的手")] GameObject surpriseTentacle; 
    [SerializeField][Tooltip("钻出手速")] float surpriseTantleSpeed = 50f;

    GameObject surpriseTantle;
    Vector2 heroPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveAfterCursor();
        if(Input.GetMouseButtonDown(0))
        {
            ActivatePoPupTentacle();
        }
        if(Input.GetMouseButtonDown(1))
        {
            ActivateSurpriseTentacle();
        }
        
    }

   

    private void ActivatePoPupTentacle()
    {
        GameObject tentacle;
        if(FindObjectsOfType<NormalTentacle>().Length < 3)
        {
			AudioCenter.Instance.Play("TantacleSpawn");
            tentacle = Instantiate(popUpTentacles[Random.Range(0,popUpTentacles.Length)], new Vector2(transform.position.x, 0), Quaternion.identity);
        }
    

    }
    private void ActivateSurpriseTentacle()
    {
        // Debug.Log("aaa");
        // heroPos = FindObjectOfType<HeroBehavior>().transform.position;
        // surpriseTantle = Instantiate(surpriseTentacle);
        // surpriseTantle.transform.position = new Vector2(rightBorder,heroPos.y);
    }
    private void MoveAfterCursor()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector2(Mathf.Clamp(mousePos.x, leftBorder,rightBorder),transform.position.y);
        transform.position = Vector2.Lerp(transform.position, mousePos, moveSpeed);
    }
}
