using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{
    MovementComponent movement;
    Joystick joy;

    private void Awake()
    {
        movement = GetComponent<MovementComponent>();
        joy = FindObjectOfType<Joystick>();
    }
    void Update()
    {
        if (joy.Horizontal != 0)
        movement.Move(joy.Horizontal);
    }
}
