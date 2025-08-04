using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //-----移動-----
    private float InitPos;


   
    void Start()
    {
        InitPos = transform.position.y;
       
    }

    void Update()
    {
        Move();
       
    }
    void Move()
    {
        transform.position = new Vector3(transform.position.x, InitPos + Mathf.PingPong(Time.time, 2.5f), transform.position.z);
    }

    // ----- 振動処理 -----
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }

}
