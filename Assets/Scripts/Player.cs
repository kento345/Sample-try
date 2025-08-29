using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    //-----移動-----
    private Vector3 vector = new Vector3(0.0f, 0.0f, 0.0f);
    private float scaler = 0.1f;
    //-----回転-----
    private Transform axis = null;
    private float move = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //移動
        vector.Set(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            vector.z = -scaler;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vector.z = scaler;
        }

        var pos = transform.position;

        pos.z += vector.z;

        transform.position = pos;

        //回転
/*        var q = Quaternion.identity;
        
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            q = Quaternion.AngleAxis(move, axis.right);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            q = Quaternion.AngleAxis(move, -axis.right);
        }
        transform.rotation = q * transform.rotation;*/
    }
}
