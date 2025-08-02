using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("弾生成")]
    //-----円状にcheckpointの生成-----
    [SerializeField] private GameObject checkpoint;               //通過点生成オブジェクト
    [SerializeField] private Transform objPos;                    //checkpoint生成する場所
    private int itemcount = 8;                                    //生成する個数                     
    private float radius = 2f;                                    //円の半径
    private float repeata = 1f;                                   //周期
    //-----Bullet生成-----
    [SerializeField] private GameObject bullePrefab_;             //弾のプレファブ
    [SerializeField] private GameObject catapalut_;               // 弾の生成場所
    public List<GameObject> FirPos  = new List<GameObject>();     //生成した弾のList
    private Bullet bullet;                                        //弾のScript
    private SpownController spownController;
    //-----敵判定-----
    private const int MaxCheckCount = 8;

    private void Start()
    {
        spownController = GetComponent<SpownController>();
        //通過点の場所生成
       CheckPoint();
       
    }
    //-----通過点を円状に配置する-----
    void CheckPoint()
    {
        //半径２の円
        var oneCycle = 2.0f * Mathf.PI;

        for (int i = 0; i < itemcount; i++)
        {
            var point = ((float)i / itemcount) * oneCycle;
            var repeapoint = point * repeata;

            var x = Mathf.Cos(repeapoint) * radius;
            var y = Mathf.Sin(repeapoint) * radius;

            var position = objPos.position + new Vector3(x, y, 0f);

            GameObject firPoint = Instantiate(checkpoint, position, Quaternion.identity, objPos.transform);
           FirPos.Add(firPoint);
          
        }
    }

    

    /*public void CheckHitByBullet(Bullet bullet)
    {
        if (spownController == null || spownController.allEnemies.Count == 0) return;

        int hitCount = 0;

        foreach (var enemy in spownController.allEnemies)
        {
            if (!enemy) continue;

            float size = 0.5f + (enemy.transform.localScale.x * 0.5f); // 弾サイズ固定なら0.5でよい

            if (Vector3.Distance(enemy.transform.position, bullet.transform.position) < size)
            {
                enemy.GetComponent<Renderer>().material.color = Color.red;
                hitCount++;
            }

            if (hitCount >= MaxCheckCount)
            {
                break;
            }
        }
    }*/

    //-----弾の生成-----
    public void BulletCrea()
    {
        for (int i = 0;i < 8;i++)
        {
            GameObject bulletObj = Instantiate(bullePrefab_, catapalut_.transform.position, Quaternion.identity);

            bullet = bulletObj.GetComponent<Bullet>();
            bullet.SetTarget(FirPos[i].transform); // 各弾に異なる通過点を設定


            //通過点通過後の敵追尾8体の敵Listをtargetに
/*          Transform neraEnemy = GetNeartEnemy(bulletObj.transform.position, remainingEnemies);
            if (i < targetEnemies.Count)
            {
                bullet.SetEnemy(neraEnemy);

                remainingEnemies.Remove(neraEnemy.gameObject);
            }*/
        }
    }

    public void CanHitEnemy(Transform player)
    {
        if (spownController == null || spownController.allEnemies.Count == 0 || player == null)
        {
            return;
        }

        List<(GameObject enemy, float dot, float dist)> candistes = new();

        Vector3 playerPos = player.position;
        Vector3 playerFwd = player.forward;

        foreach (var enemy in spownController.allEnemies)
        {
            if (!enemy)
            { continue; }

            Vector3 toEnemy = (enemy.transform.position - playerPos).normalized;
            float dot = Vector3.Dot(playerFwd.normalized, toEnemy);

            if (dot > 0.9f)
            {
                float distance = Vector3.Distance(playerPos, enemy.transform.position);
                candistes.Add((enemy, dot, distance));
            }
        }

        foreach (var enemy in spownController.allEnemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<Renderer>().material.color = Color.white;
            }
        }

        var sorted = candistes.OrderByDescending(enabled => enabled.dot).ThenBy(enabled => enabled.dist).Take(8);

        foreach (var (enemy, _, _) in sorted)
        {
            if(enemy != null)
            {
                enemy.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }

    //-----一番近い敵の判定-----
    Transform GetNeartEnemy(Vector3 fromPosition, List<GameObject> candidates)
    {
        float minDistance = Mathf.Infinity;
        Transform closest = null;

        foreach (var enemyObj in candidates)
        {
            if (enemyObj == null) continue;

            float dist = Vector3.Distance(fromPosition, enemyObj.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = enemyObj.transform;
            }
        }

        return closest;
    }

}
    