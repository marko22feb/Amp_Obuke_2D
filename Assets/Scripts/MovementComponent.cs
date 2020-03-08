using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    /*
     * My English isn't perfect and it's self taught. Reason I am writing and commenting everything in English is to create a good practice. As you will often be in a situation where you will work with non-Slavs.
     * Whether as a freelancer or due to outsourcing. Grammar doesn't need to be perfect, it's enough to be understood by others.
     */
    
    private Transform tr;
    public Rigidbody2D rigidbody2d;
    private Animator anim;
    private RaycastHit2D raycasthit2D;
    public BoxCollider2D groundBoxcollider2D;
    private Vector2 colliderSize;
    private Vector2 colliderOffset;
    //I did not set LayerMask to be default in class, and only reason I set it to default is to avoid annoying false warning.
    [SerializeField] private LayerMask floorLayerMask = default;

    public bool IsUsingUIinput = false;
    public float Speed = 5;
    public float airControl = .5f;
    public float JumpHeight = 500;
    public GameObject projectile;

    private void Awake()
    {
        Time.timeScale = 1;
    }
    void Start()
    {
        //Getting the references of variables from scene. All these variables are components added to GameObject by name of Player.
        tr = GetComponent<Transform>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundBoxcollider2D = transform.GetChild(0).GetComponent<BoxCollider2D>();
        colliderSize = groundBoxcollider2D.size;
        colliderOffset = groundBoxcollider2D.offset;
    }

    private void Update()
    {
        //Sets the boolean inside of Player Animator based on the IsOnGround() function.
        anim.SetBool("IsOnGround", IsOnGround());

        //If Input "Jump" is pressed the function Jump() will be called. Inputs can be found in Edit>Project Settings>Input Manager
        if (Input.GetAxis("Jump") > 0)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject temp;
            temp = Instantiate(projectile);
            temp.GetComponent<projectile>().x = rigidbody2d.velocity.x;
            temp.GetComponent<projectile>().y = rigidbody2d.velocity.y;
        }

        if (!IsUsingUIinput)
        {
            if (Input.GetButtonDown("Crouch"))
            {
                Crouch(true);
            }

            if (Input.GetAxis("Crouch") == 0)
            {
                Crouch(false);
            }
        }
    }

    void FixedUpdate()
    {
        /*
         * If Player is on ground, decided by IsOnGround() function, the movement input (in our case: A,D,Left Arrow, Right Arrow. Again decided in Edit>Project Settings>Input Manager)
         * will be enabled. if Player is above ground, movement input will be disabled.
         * Movement is set using Physics2D.(RigidBody2D component found on our Player)
         * We set the velocity(Vector2) X to our preferred speed of movement, Y to current value since Y dictates how high the character will go.
         * We set Player rotation based on where we are moving. Finally we set "IsRunning" boolean inside of Player Animator to true.
         */
        if (Input.GetAxis("Horizontal") != 0)
        {
            Move(Input.GetAxis("Horizontal"));
            
        }

        //If we are not holding down movement buttons then we are setting boolean "IsRunning" inside of Player Animator to false.
        if (Input.GetAxis("Horizontal") == 0)
        {
            anim.SetBool("IsRunning", false);
        }
    }

    public void Move(float move)
    {
        if (IsOnGround())
            rigidbody2d.velocity = new Vector2(move * Speed, rigidbody2d.velocity.y);
        else
        {
            rigidbody2d.velocity += new Vector2(move * Speed * airControl, 0);
            rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -Speed, Speed), rigidbody2d.velocity.y);
        }

        if (move > 0)
            tr.rotation = new Quaternion(0, 0, 0, 0);
        else tr.rotation = new Quaternion(0, 180, 0, 0);
        anim.SetBool("IsRunning", true);
    }

    private bool IsOnGround()
    {
        /*
        This will cast a ray in a form of BoxCollider under the Player, and if the floor is detected it will return IsOnGround true.
        First input it needs is from where the raycast start, in this case from center of BoxColliders bounds.
        Second input is the size of Raycast, in this case it's same size as bounds of BoxColliders. (Box Collider is only as big as it's bounds)
        Third value is the direction of the ray. Straight down.
        Forth is how far the ray will go. In this case it's 0.1f, so just a little bit under Players feet.
        And fifth input is what it will be able to hit, based on a layer mask.
        */
        raycasthit2D = Physics2D.BoxCast(groundBoxcollider2D.bounds.center, groundBoxcollider2D.bounds.size, 0f, Vector2.down, 0.1f, floorLayerMask);
        if (raycasthit2D.collider != null)
        {
            return true;
        }
        else return false;
    }
    public void Jump()
    {
        //If Player is on ground changed Psyhics velocity.Y to selected JumpHeight while velocity.X remains as is.
        if (IsOnGround())
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, JumpHeight);
        }
    }

    public void Crouch(bool IsCrouching)
    {
        //If on ground while Crouch is pressed set the boolean inside animation to same value as bool IsCrouching from the functions input.
        if (IsOnGround())
        {
            anim.SetBool("IsCrouching", IsCrouching);
        }

        if (IsCrouching)
        {
            //If crouching is true, set the height of the collider to be half of set size and it's wight to be 50% wider. also lower the collider a bit.
            //This is because once he crouches in the animation his width gets .. wider. There is another way to do it, but this will suffice.
            groundBoxcollider2D.size = new Vector2(colliderSize.x / 2, colliderSize.y * 1.5f);
            groundBoxcollider2D.offset = new Vector2(colliderOffset.x, 0f);
            //We cast a raycast to check if under is a board we can fall through.
            raycasthit2D = Physics2D.BoxCast(groundBoxcollider2D.bounds.center, groundBoxcollider2D.bounds.size, 0f, Vector2.down, 0.1f, floorLayerMask);
            if (raycasthit2D.collider != null)
            {
                if (raycasthit2D.collider.tag == "Board")
                {
                    OnJumpTrigger trigger = raycasthit2D.collider.gameObject.GetComponent<OnJumpTrigger>();
                    trigger.enabled = false;
                    foreach (GameObject gob in trigger.Neighbours)
                    {
                        gob.GetComponent<OnJumpTrigger>().enabled = false;
                    }
                    //Collider is set to be a trigger, which means we can pass through a collider and physics are ignored.
                    trigger.ChangeTriggerValue(true);
                    //Start the IEnumerator EnableAgain().
                    StartCoroutine(EnableAgain(trigger));
                }
            }
        }
        else
        {
            //If we are not crouching return the size and offset of collider to normal.
            groundBoxcollider2D.size = colliderSize;
            groundBoxcollider2D.offset = colliderOffset;
        }
    }

    //This simply gives you an option to delay a line of code. In this case we are enabling Update function in the Boards <OnJumpTrigger> component.
    IEnumerator EnableAgain(OnJumpTrigger trigger)
    {
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject gob in trigger.Neighbours)
        {
            gob.GetComponent<OnJumpTrigger>().enabled = true;
        }

        StopCoroutine(EnableAgain(trigger));
    }
}
