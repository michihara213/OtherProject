using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 6f;       // 左へ流れる速さ
    public float destroyX = -20f;  // ここより左に行ったら破棄

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}