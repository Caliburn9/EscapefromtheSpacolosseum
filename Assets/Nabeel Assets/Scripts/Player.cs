using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    Normal,
    Powered
}

public class Player : MonoBehaviour
{
    public float speed, jumpForce, raycastDist;
    float horInput, vertInput;
    Rigidbody2D rb;
    public LayerMask ladder;
    bool isGrounded, isClimbing;
    public float poweredTimer;
    PlayerState state;
    static float startTime = 0.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Powerup")
        {
            collision.gameObject.SetActive(false);
            state = PlayerState.Powered;
            Debug.Log("Powered Up");
        }

        if (collision.tag == "Score")
        {
            ScoreManager.increaseScore(1);
        }
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            if (state == PlayerState.Normal)
            {
                SceneManager.LoadScene("Game1Level1");
            } else 
            if (state == PlayerState.Powered)
            {
                collision.gameObject.SetActive(false);
                ScoreManager.increaseScore(2);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = PlayerState.Normal;
    }

    void PoweredStateTimer(float timer)
    {
        startTime += Time.deltaTime * 2.0f;

        if (startTime >= timer && state == PlayerState.Powered)
        {
            state = PlayerState.Normal;
            Debug.Log("Back to Normal");
        }
    }

    private void FixedUpdate()
    {
        horInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horInput * speed, rb.velocity.y);
    }

    private void Update()
    {
        isGrounded = rb.velocity.y < .1f && rb.velocity.y > -.1f;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, raycastDist, ladder);
        
        if (hitInfo.collider != null)
        {
            if (state != PlayerState.Powered)
            {
                //Climb
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    isClimbing = true;
                }
            }
        } else
        {
            isClimbing = false;

            //Jump
            if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
        }

        if (isClimbing && hitInfo.collider != null)
        {
            vertInput = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.transform.position.x, vertInput * (speed-1));
            rb.gravityScale = 0;
        } else
        {
            rb.gravityScale = 1;
        }

        if (state == PlayerState.Powered)
        {
            PoweredStateTimer(poweredTimer);
        }
    }
}
