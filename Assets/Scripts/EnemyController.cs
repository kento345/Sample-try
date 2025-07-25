using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //-----移動-----
    private float InitPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitPos = transform.position.y;
       
    }

    // Update is called once per frame
    void Update()
    {
        Move();
       
    }
    void Move()
    {
        transform.position = new Vector3(transform.position.x, InitPos + Mathf.PingPong(Time.time, 2.5f), transform.position.z);
    }
   
}
