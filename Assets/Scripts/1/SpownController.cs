using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SpownController : MonoBehaviour
{
    //敵プレファブ
    [SerializeField] private GameObject enemyPrefab;
    //生成したすべての敵List 
    public List<GameObject> allEnemies = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < 24; i++)
        {
            //生成する範囲
            Vector3 spawnPosition = new Vector3(Random.Range(-30f, 30f), Random.Range(-5f, 5f), Random.Range(20f, 15f));
            //敵1体の生成
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            //生成した敵をListに挿入
            allEnemies.Add(enemy);
        }
    }
}
