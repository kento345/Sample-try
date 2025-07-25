using System.Runtime.InteropServices;
using UnityEngine;

public class SpownController : MonoBehaviour
{
    [SerializeField] private GameObject enemy_;
    private int maxEnemy = 24;
    private int CountEnemy = 0;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Spow();
    }
    void Spow()
    {
        if(CountEnemy < maxEnemy)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-35f, 35f), Random.Range(-10f,0f), 15f);

            Instantiate(enemy_, spawnPosition, transform.rotation);
            CountEnemy++;
        }
        
    }
}
