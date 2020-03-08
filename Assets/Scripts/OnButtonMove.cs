using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButtonMove : MonoBehaviour
{
    public MovementComponent movement;

    private void Awake()
    {
        movement = GameObject.Find("Player").GetComponent<MovementComponent>();
    }

    public void MoveLeft() {
        movement.Move(-1);
        Debug.Log("clicked");
    }

    public void MoveRight(){
        movement.Move(1);
    }
}
