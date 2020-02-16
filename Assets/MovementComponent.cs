using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{

    private Transform tr;
    private Rigidbody2D rigidbody2d;
    private Animator anim;
    private RaycastHit2D raycasthit2D;
    private BoxCollider2D boxcollider2D;
    [SerializeField] private LayerMask floorLayerMask;

    public float Speed = 5;
    public float JumpHeight = 500;

    void Start()
    {
        tr = GetComponent<Transform>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxcollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        anim.SetBool("IsOnGround", IsOnGround());

        if (Input.GetAxis("Jump") > 0)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (IsOnGround())
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                rigidbody2d.velocity = new Vector2(Input.GetAxis("Horizontal") * Speed, rigidbody2d.velocity.y);
                tr.rotation = new Quaternion(0, 0, 0, 0);
                anim.SetBool("IsRunning", true);
            }

            if (Input.GetAxis("Horizontal") < 0)
            {
                rigidbody2d.velocity = new Vector2(Input.GetAxis("Horizontal") * Speed, rigidbody2d.velocity.y);
                tr.rotation = new Quaternion(0, 180, 0, 0);
                anim.SetBool("IsRunning", true);
            }
        }

        if (Input.GetAxis("Horizontal") == 0)
        {
            anim.SetBool("IsRunning", false);
        }
    }

    private bool IsOnGround()
    {
        raycasthit2D = Physics2D.BoxCast(boxcollider2D.bounds.center, boxcollider2D.bounds.size, 0f, Vector2.down, 0.1f, floorLayerMask);
        if (raycasthit2D.collider != null)
        {
            return true;
        }
        else return false;
    }
    private void Jump()
    {
        if (IsOnGround())
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, JumpHeight);
        }
    }
}
