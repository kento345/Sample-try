using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //-----移動-----
    private float scalar_ = 0.1f;
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
        var pos = transform.position;

        if(Input.GetKey(KeyCode.W))
        {
            pos.z += scalar_; 
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.z -= scalar_;
        }
        transform.position = pos;
    }
}
