using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpButton : MonoBehaviour
{
    MovementComponent movement;

    private void Awake()
    {
        movement = GameObject.Find("Player").GetComponent<MovementComponent>();
    }
    public void OnClick()
    {
        movement.Jump();
    }
}
