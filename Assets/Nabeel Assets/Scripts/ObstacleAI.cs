using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleAI : MonoBehaviour
{
    float moveSpeed = 5f;
    public bool moveRight;
    Rigidbody2D rb;
    Animator anim;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatisWall;
    bool wallCollided;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name == "Game1L3")
        {
            moveRight = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Deathzone")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        wallCollided = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatisWall);

        if (wallCollided)
        {
            moveRight = !moveRight;
        }

        if (moveRight)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); 
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            anim.SetBool("Right", true);
        } else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            anim.SetBool("Right", false);
        }
    }
}
