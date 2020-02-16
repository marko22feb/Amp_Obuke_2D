﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    /*
     * My English isn't perfect and it's self taught. Reason I am writing and commenting everything in English is to create a good practice. As you will often be in a situation where you will work with non-Slavs.
     * Whether as a freelancer or due to outsourcing. Grammar doesn't need to be perfect, it's enough to be understood by others.
     */
    private Transform tr;
    private Rigidbody2D rigidbody2d;
    private Animator anim;
    private RaycastHit2D raycasthit2D;
    private BoxCollider2D boxcollider2D;
    //I did not set LayerMask to be default in class, and only reason I set it to default is to avoid annoying false warning.
    [SerializeField] private LayerMask floorLayerMask = default;

    public float Speed = 5;
    public float JumpHeight = 500;

    void Start()
    {
        //Getting the references of variables from scene. All these variables are components added to GameObject by name of Player.
        tr = GetComponent<Transform>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxcollider2D = GetComponent<BoxCollider2D>();
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
        //If we are not holding down movement buttons then we are setting boolean "IsRunning" inside of Player Animator to false.
        if (Input.GetAxis("Horizontal") == 0)
        {
            anim.SetBool("IsRunning", false);
        }
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
        raycasthit2D = Physics2D.BoxCast(boxcollider2D.bounds.center, boxcollider2D.bounds.size, 0f, Vector2.down, 0.1f, floorLayerMask);
        if (raycasthit2D.collider != null)
        {
            return true;
        }
        else return false;
    }
    private void Jump()
    {
        //If Player is on ground changed Psyhics velocity.Y to selected JumpHeight while velocity.X remains as is.
        if (IsOnGround())
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, JumpHeight);
        }
    }
}
