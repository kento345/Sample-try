using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Jobs;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //-----移動-----
    private Vector3 vector = new Vector3(0.0f,0.0f, 0.0f);
    private const float scalar = 0.1f;

    //-----回転-----
    private int move = 1;
    private float yaw = 0.0f;   // 左右回転
    private float pitch = 0.0f; // 上下回転
    private float rotateSpeed = 1.0f;

    private Transform camera_;

   //[SerializeField] private GameObject bullet_;

   

    //-----その他-----
    [SerializeField] private GameController GMController;


    //[SerializeField] private Bullet bullet;

    private bool isHighlighting = false;
    public void OnShot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
           isHighlighting = true;
        }
        if (context.canceled)
        {
            GMController.BulletCrea();
            isHighlighting = false;
        }
    }
    private void Awake()
    {
        
    }

    private void Start()
    {
        camera_ = Camera.main.transform;
    }

    private void Update()
    {
        Move();
        Rotate();

        if (isHighlighting)
        {
            GMController.CanHitEnemy(transform);
        }
    }

    void Move()
    {
        vector.Set(0,0,0);
        if (Input.GetKey(KeyCode.W))
        {
            vector.z = scalar;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vector.z = -scalar;
        }

        Vector3 forward = new Vector3(camera_.forward.x, 0, camera_.forward.z).normalized;
        transform.position += transform.forward * vector.z;
    }
    void Rotate()
    {
        // 入力に応じて角度加算
        if (Input.GetKey(KeyCode.LeftArrow))
            yaw -= rotateSpeed;
        if (Input.GetKey(KeyCode.RightArrow))
            yaw += rotateSpeed;
        if (Input.GetKey(KeyCode.UpArrow))
            pitch -= rotateSpeed;
        if (Input.GetKey(KeyCode.DownArrow))
            pitch += rotateSpeed;

        // 上下の回転角度を制限（ぐるぐる回らないように）
        pitch = Mathf.Clamp(pitch, -80f, 80f);

        // 回転適用
        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }

}

