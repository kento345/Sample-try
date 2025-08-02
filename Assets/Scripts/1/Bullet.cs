using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Bullet : MonoBehaviour
{
    //-----弾の移動-----
    private Transform target;                   // 通過点ターゲット          
    public float speed = 5f;                    //移動スピード
    public float rotateSpeed = 360f;　　　　　　//回転スピード
    public float targetReachThreshold = 0.2f;   //オブジェクト同士の距離
    private bool CheckPoint = false;            //通過したかのフラグ
    private Vector3 currentDirection;           //移動ベクトル
    private Transform targetEnemy;              //敵ターゲット

    public float radius => transform.localScale.x * 0.5f;

    void Start()
    {
       
        //初期進行方向を前方に設定
        currentDirection = transform.forward;
    }
    private void Update()
    {
        BulletMove();
    }
    //通過点の場所のListをtargertに取得
    public void SetTarget(Transform t)
    {
        target = t;
    }
    //ロックオンされた敵ListをtargetEnemyに取得
    public void SetEnemy(Transform te)
    {
        targetEnemy = te;
    }
    //弾を通過点を通過させて敵にホーミングさせる
    void BulletMove()
    {
        
        if (target == null && !CheckPoint)
        {
            Destroy(gameObject);
            return;
        }
        //旋回最大角度(20度)
        var inViewCos = Mathf.Cos(20.0f * Mathf.Deg2Rad);
        //通過点通過
        if (!CheckPoint)
        {
            //目標方向
            var toTarget = (target.position - transform.position).normalized;
            
            currentDirection = Vector3.RotateTowards(currentDirection,toTarget,inViewCos,0.0f);
            //回転
            transform.forward = currentDirection;
            //前進
            transform.position += currentDirection * speed * Time.deltaTime;
            //通過点に到達したかの判定フラグ
            if (Vector3.Distance(transform.position, target.position) < targetReachThreshold)
            {
                 CheckPoint= true;
            }
        }
        //通過点通過後処理
        else if (targetEnemy != null)
        {
            //目標方向
            Vector3 toEnemy = (targetEnemy.position - transform.position).normalized;

            currentDirection = Vector3.RotateTowards(currentDirection, toEnemy, inViewCos, 0.0f);
            //回転
            transform.forward = currentDirection;
            //前進
            transform.position += currentDirection * speed * Time.deltaTime;
        }
 /*       GameObject controllerObj = GameObject.FindWithTag("GameController");
        if (controllerObj != null)
        {
            GameController controller = controllerObj.GetComponent<GameController>();
            if (controller != null)
            {
              
                controller.CheckHitByBullet(this); // 新規に作る処理（後述）
            }
        }*/
    }
}

