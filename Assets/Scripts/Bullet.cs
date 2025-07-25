using System.Threading.Tasks;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField] private float speed_;

    /// <summary>
    /// 半径
    /// </summary>
    private float radius_ = 0.0f;

    /// <summary>
    /// 移動ベクトル
    /// </summary>
    private Vector3 moveVector_ = Vector3.zero;

    /// <summary>
    /// 半径を取得する
    /// </summary>
    public float radius { get { return radius_; } }

    /// <summary>
    /// 発射する
    /// <summary>
    public async void Fire(Vector3 forwerd)
    {
        moveVector_ = forwerd.normalized * speed_;
        await Move();

        if (gameObject)
        {
            Object.Destroy(gameObject);
        }
    }

    /// <summary>
    /// 開始処理
    /// </summary>
    private void Start()
    {
        radius_ = transform.localScale.x * 0.5f;
    }


    /// <summary>
    /// 移動処理
    /// </summary>
    private async Task Move()
    {
        var moveTime = 0.0f;

        // 5 秒間移動する
        while (moveTime < 5.0f && !Application.exitCancellationToken.IsCancellationRequested)
        {
            transform.position += moveVector_;
            if (GameController.instance.CheckHitEnemy(this))
            {
                // 敵にヒットしたら移動を終了する
                break;
            }

            moveTime += Time.deltaTime;
            await Task.Yield();
        }
    }

}
