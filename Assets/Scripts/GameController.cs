using UnityEngine;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// シングルトンインスタンス（全体で唯一のインスタンス）
    /// </summary>
    private static GameController instance_;

    /// <summary>
    /// 敵オブジェクト
    /// </summary>
    [SerializeField] private GameObject enemy_;

    /// <summary>
    /// シングルトンインスタンスの取得
    /// </summary>
    public static GameController instance
    {
        get
        {
            if (instance_ == null)
            {
                instance_ = new GameController();
            }
            return instance_;
        }
    }


    /// <summary>
    /// 敵に弾が当たるかどうかをチェックする
    /// </summary>
    /// <returns></returns>
    public void CanHitEnemy(Bullet bullet)
    {
        if (!enemy_)
        {
            return;
        }

        var playerToEnemy = (enemy_.transform.position - bullet.transform.position);
        var dot = Vector3.Dot(bullet.transform.forward, playerToEnemy);
        var nearPos = bullet.transform.position + (bullet.transform.forward * dot);

        var size = bullet.radius + (enemy_.transform.localScale.x * 0.5f);

        if (Vector3.Distance(enemy_.transform.position, nearPos) < size)
        {
            enemy_.GetComponent<MeshRenderer>().materials[0].color = Color.red;
        }
        else
        {
            enemy_.GetComponent<MeshRenderer>().materials[0].color = Color.white;
        }
    }

    /// <summary>
    /// 敵に弾が当たったかどうかをチェックする
    /// </summary>
    /// <returns></returns>
    public bool CheckHitEnemy(Bullet bullet)
    {
        if (!enemy_)
        {
            return false;
        }

        var size = bullet.radius + (enemy_.transform.localScale.x * 0.5f);

        if (Vector3.Distance(enemy_.transform.position, bullet.transform.position) < size)
        {
            ResetEnemy();
            return true;
        }

        return false;
    }

    /// <summary>
    /// 敵をリセットする
    /// </summary>
    /// <returns></returns>
    public async void ResetEnemy()
    {
        // 今いる敵を削除する
        if (enemy_)
        {
            Object.Destroy(enemy_);
            enemy_ = null;
        }

        // 1秒待つ
        await Awaitable.WaitForSecondsAsync(1f);

  
    }


    /// <summary>
    /// 敵を生成する
    /// </summary>
    

    /// <summary>
    /// コンストラクタ
    /// シングルトンクラスなのでプライベート化する
    /// </summary>

}
