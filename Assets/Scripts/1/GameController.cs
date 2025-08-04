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
    private List<GameObject> redEnemies = new List<GameObject>(); //赤くしている敵のList

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

    //-----弾の生成-----
    public void BulletCrea()
    {
        for (int i = 0;i < 8;i++)
        {
            GameObject bulletObj = Instantiate(bullePrefab_, catapalut_.transform.position, Quaternion.identity);

            bullet = bulletObj.GetComponent<Bullet>();
            bullet.SetTarget(FirPos[i].transform); // 各弾に異なる通過点を設定


            //通過点通過後の敵追尾8体の敵Listをtargetに

            if (i < redEnemies.Count)
            {
                bullet.SetEnemy(redEnemies[i].transform);
            }
        }
    }

    public void CanHitEnemy(Transform player)
    {
        if (spownController == null || spownController.allEnemies.Count == 0 || player == null)
        {
            return;
        }

        redEnemies.Clear();

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
                redEnemies.Add(enemy);
            }
        }
    }
}
    