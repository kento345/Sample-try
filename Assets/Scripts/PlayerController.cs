using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //-----移動-----
    private Vector3 vector_ = new Vector3(0.0f, 0.0f, 0.0f);

    private const float scalar_ = 0.1f;
    //-----回転-----
    private Transform player_;
    private Transform camera_;

    private float move_ = 1.0f;
    //-----視野角-----
    private EnemyController enemyController_;



    //-----弾発射-----



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera_ = Camera.main.transform;
        player_ = GetComponent<Transform>();
        enemyController_ = GetComponent<EnemyController>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rota();
        Viewing();
    }
    private void LateUpdate()
    {
/*        for (int i = 0; i < 8; i++)
        {
            if (!bullets[i])
            {
                return;
            }

            GameController.instance.CanHitEnemy(bullets[i]);
        }*/
    }
    void Move()
    {
        Vector3 pos = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            pos += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
           pos -= transform.forward;
        }

        transform.position += pos.normalized * scalar_;
    }
    void Rota()
    {
        var q = Quaternion.identity;
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {     
                q = Quaternion.AngleAxis(-move_, camera_.up);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            q = Quaternion.AngleAxis(move_, camera_.up);
        }
       
        if (Input.GetKey(KeyCode.UpArrow))
        {
            q = Quaternion.AngleAxis(-move_, camera_.right);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            q = Quaternion.AngleAxis(move_, camera_.right);
        }

        transform.rotation = q * transform.rotation;
    }
    void Viewing()
    {
     
    }
}
