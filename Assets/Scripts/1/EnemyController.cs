using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //-----移動-----
    private float InitPos;


   
    void Start()
    {
        InitPos = transform.position.y;
       
    }

    void Update()
    {
        Move();
       
    }
    void Move()
    {
        transform.position = new Vector3(transform.position.x, InitPos + Mathf.PingPong(Time.time, 2.5f), transform.position.z);
    }
   
}
