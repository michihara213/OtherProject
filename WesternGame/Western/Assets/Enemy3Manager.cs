using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Manager : MonoBehaviour
{
    public int maxHP = 5;   // 敵ごとのHP
    int currentHP;

    public GameObject EnemyBulletPrefab;
    public GameObject FirePoint3;

    public float speed = 6f;       // 左へ流れる速さ
    public float destroyX = -20f;  // ここより左に行ったら破棄

    public int enemyPoint;  // 敵を倒した際のポイントを決める変数

    void Start()
    {
        currentHP = maxHP;

        InvokeRepeating("Shot", 1f, 2f); // 1秒後から2秒ごとに弾を発射
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }

    private void Shot()
    {
        Instantiate(EnemyBulletPrefab, FirePoint3.transform.position, transform.rotation);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        
        if (currentHP <= 0)
        {
            ScoreManager.instance.AddScore(enemyPoint);
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject); // 倒されたら消える
    }
}