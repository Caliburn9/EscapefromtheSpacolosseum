using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    bool isGrounded, isClimbing, canClimb;
    PlayerState state;
    Animator anim;
    static float startTime = 0.0f;

    //Public 
    public float speed, jumpForce, raycastDist;
    public float poweredTimer;
    public LayerMask ladder;
    public int ScoreAmount;
    public LevelManager levelManager;
    public AnimatorOverrideController normPlayer, poweredPlayer;
    public PlayerFeet playerFeet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Powerup")
        {
            Destroy(collision.gameObject);
            state = PlayerState.Powered;
            Debug.Log("Powered Up");
        }

        if (collision.tag == "Score" && isClimbing != true)
        {
            ScoreManager.increaseScore(ScoreAmount);
        }

        if (collision.tag == "Door")
        {

            levelManager.LoadNextLevel();
        }

        if (collision.tag == "Ladder")
        {
            canClimb = true;
        }

        //Death
        if (collision.tag == "Deathzone")
        {
            levelManager.ScoreMenu();
            anim.SetBool("isDead", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
        {
            canClimb = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            //Death
            if (state == PlayerState.Normal)
            {
                levelManager.ScoreMenu();
                anim.SetBool("isDead", true);
            }
            else 
            if (state == PlayerState.Powered)
            {
                Destroy(collision.gameObject);
                ScoreManager.increaseScore(ScoreAmount + 1);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        state = PlayerState.Normal;
        canClimb = false;
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
        anim.SetFloat("Speed", Mathf.Abs(horInput));
        anim.SetBool("isClimbing", isClimbing);

        if (playerFeet.onGround == true)
        {
            anim.SetBool("isJumping", false);
        } else if (playerFeet.onGround == false)
        {
            anim.SetBool("isJumping", true);
        }

        if (horInput > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (horInput < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        isGrounded = rb.velocity.y < .1f && rb.velocity.y > -.1f;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, raycastDist, ladder);

        if (hitInfo.collider != null && canClimb == true)
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
        if (canClimb == false)
        {
            isClimbing = false;

            //Jump
            if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded && canClimb == false)
            {
                rb.velocity = Vector2.up * jumpForce;
                playerFeet.onGround = false;
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
            anim.runtimeAnimatorController = normPlayer as RuntimeAnimatorController;
        } else if (state == PlayerState.Powered)
        {
            anim.runtimeAnimatorController = poweredPlayer as RuntimeAnimatorController;

        }
    }
}
