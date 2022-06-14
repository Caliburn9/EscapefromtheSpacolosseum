using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed, jumpForce, raycastDist;
    float horInput, vertInput;
    Rigidbody2D rb;
    public LayerMask ladder;
    bool isGrounded, isClimbing;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Powerup")
        {
            collision.gameObject.SetActive(false);
            Debug.Log("Shielded");
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            //Climb
            if (Input.GetKey(KeyCode.UpArrow))
            {
                isClimbing = true;
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
    }
}
