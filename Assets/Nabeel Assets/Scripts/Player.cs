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
    //Private
    float horInput, vertInput;
    Rigidbody2D rb;
    bool isGrounded, isClimbing;
    PlayerState state;
    SpriteRenderer spriteRender;
    static float startTime = 0.0f;

    //Public 
    public float speed, jumpForce, raycastDist;
    public Sprite normalSpr, poweredSpr;
    public float poweredTimer;
    public string currLevel, nextLevel;
    public LayerMask ladder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Powerup")
        {
            Destroy(collision.gameObject);
            state = PlayerState.Powered;
            Debug.Log("Powered Up");
        }

        if (collision.tag == "Score")
        {
            ScoreManager.increaseScore(1);
        }

        if (collision.tag == "Door")
        {
            
            SceneManager.LoadScene(nextLevel);
        }

        if (collision.tag == "Deathzone")
        {
            SceneManager.LoadScene(currLevel);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            if (state == PlayerState.Normal)
            {
                SceneManager.LoadScene(currLevel);
            } else 
            if (state == PlayerState.Powered)
            {
                Destroy(collision.gameObject);
                ScoreManager.increaseScore(2);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        state = PlayerState.Normal;
        spriteRender.sprite = normalSpr;
    }

    void PoweredStateTimer(float timer)
    {
        startTime += Time.deltaTime * 2.0f;

        if (startTime >= timer && state == PlayerState.Powered)
        {
            state = PlayerState.Normal;
            Debug.Log("Back to Normal");
            startTime = 0.0f;
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

        if (state == PlayerState.Normal)
        {
            spriteRender.sprite = normalSpr;
        } else if (state == PlayerState.Powered)
        {
            spriteRender.sprite = poweredSpr;
        }
    }
}
