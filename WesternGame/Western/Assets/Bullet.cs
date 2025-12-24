using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;      // 弾速
    public float lifeTime = 2f;    // 何秒で消えるか

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    // 当たり判定
    void OnTriggerEnter2D(Collider2D other)
    {
        // Enemy1
        var e1 = other.GetComponent<Enemy1Manager>();
        if (e1 != null)
        {
            e1.TakeDamage(1);
            Destroy(gameObject);
            return;
        }

        // Enemy2
        var e2 = other.GetComponent<Enemy2Manager>();
        if (e2 != null)
        {
            e2.TakeDamage(1);
            Destroy(gameObject);
            return;
        }

        // Enemy3
        var e3 = other.GetComponent<Enemy3Manager>();
        if (e3 != null)
        {
            e3.TakeDamage(1);
            Destroy(gameObject);
            return;
        }

        // サボテンに当たったら弾だけ消す
        if (other.CompareTag("Cactus"))
        {
            Destroy(gameObject);
        }

        // 敵の弾に当たったら両方の弾を消す
        if (other.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}