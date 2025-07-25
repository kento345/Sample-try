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
    //-----弾発射-----
    [SerializeField] private GameObject bulletPrefab_;
    private Bullet bullet_;
    private GameObject catapult_;
    //[SerializeField] private InputAction fire_;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera_ = Camera.main.transform;
        player_ = GetComponent<Transform>();
        FindCatapult();
        InstantiateBullet();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rota();
    }
    private void LateUpdate()
    {
        if (!bullet_)
        {
            return;
        }

        GameController.instance.CanHitEnemy(bullet_);
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
                q = Quaternion.AngleAxis(-move_, player_.up);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            q = Quaternion.AngleAxis(move_, player_.up);
        }
       
        if (Input.GetKey(KeyCode.UpArrow))
        {
            q = Quaternion.AngleAxis(-move_, player_.right);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            q = Quaternion.AngleAxis(move_, player_.right);
        }

        transform.rotation = q * transform.rotation;
    }
    public void OnFirePerformed(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Canceled)
        {
            bullet_.transform.SetParent(null);
            bullet_.Fire(catapult_.transform.forward);

            bullet_ = null;
            InstantiateBullet();
        }
    }
    void FindCatapult()
    {
        catapult_ = transform.Find("Catapult")?.gameObject;
    }
    void InstantiateBullet()
    {
        if(bulletPrefab_ != null)
        {
            bullet_ = Instantiate(bulletPrefab_, catapult_.transform)?.GetComponent<Bullet>();
        }
    }
}
