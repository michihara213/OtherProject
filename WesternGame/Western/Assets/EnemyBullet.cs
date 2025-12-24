using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;      // 弾速
    public float lifeTime = 2f;    // 何秒で消えるか

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
