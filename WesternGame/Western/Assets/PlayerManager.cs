using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// キーを押すと、移動する（ジャンプ版）
public class PlayerJump : MonoBehaviour 
{
	//-------------------------------------
    public KeyCode jumpKey = KeyCode.Space; // プルダウンメニューで選択するキー
    public float speed = 5f; //［スピード］
    public float jumppower = 8f; //［ジャンプ力］
    public float checkDistance = 0.1f; //［地面チェックの距離］
    public float footOffset = 0.01f; //［足元位置のオフセット］
    //-------------------------------------

    public GameObject bulletPrefab;
    public GameObject firePoint;

    Rigidbody2D rbody;
    float vx = 0;
    bool leftFlag;
    bool isGrounded;
    bool isJumping = false;

    void Start() 
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update() 
    {
        // 足の下が何かに触れているかを調べる
        float myHeight = GetComponent<Collider2D>().bounds.extents.y;
        float footy = transform.position.y - myHeight - footOffset;
        Vector2 startRay = new Vector2(transform.position.x, footy);
        isGrounded = Physics2D.Raycast(startRay, Vector2.down, checkDistance);

        // ジャンプキーが押されて、着地していて、ジャンプ中でなければジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping) 
        {
            isJumping = true;
            rbody.AddForce(new Vector2(0, jumppower), ForceMode2D.Impulse);
        }
        if (rbody.velocity.y <= 0) 
        {
            isJumping = false; // 上昇をやめたらジャンプ中を解除
        }

        Shot();
    }

    public void Shot()
    {
        if ((Input.GetKeyDown(KeyCode.Return)) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Instantiate(bulletPrefab, firePoint.transform.position, transform.rotation);
        }
    }

    // 接触判定 & 画面遷移
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            SceneManager.LoadScene("OverScene");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Enemy") ||
            col.collider.CompareTag("Cactus"))
        {
            SceneManager.LoadScene("OverScene");
        }
    }

}