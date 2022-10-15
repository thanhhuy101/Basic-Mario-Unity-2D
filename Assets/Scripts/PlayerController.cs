using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private enum State { idle, run, jump, falling };
    private State state = State.idle;
    private Collider2D coll;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float Direction = Input.GetAxis("Horizontal");
        if(Direction > 0)
        {
            rb.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector2(1, 1);
            
        }

        else if (Direction < 0)
        {
            rb.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector2(-1, 1);
            
        }
        else
        {
            
        }

        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(Ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jump;
        }

        VelocityState();
        anim.SetInteger("State", (int)state);
    }

    private void VelocityState()
    {
        if (state == State.jump)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(Ground))
            {
                state = State.idle;
            }
        }

        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.run;
        }
        else
        {
            state = State.idle;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
        }
    }
}
