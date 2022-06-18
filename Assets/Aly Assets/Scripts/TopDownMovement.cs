using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopDownMovement : MonoBehaviour
{
    
    public Rigidbody2D rb2d;
    private Vector2 moveInput;

    [SerializeField]
    float moveSpeed = 3f;

    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CircleCollider")
        {
            SceneManager.LoadScene(3);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb2d.velocity = moveInput * moveSpeed;
    }
}
