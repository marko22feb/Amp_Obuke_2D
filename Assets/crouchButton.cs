using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class crouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    MovementComponent movement;
    bool buttonPressed;

    void Awake()
    {
        movement = GameObject.Find("Player").GetComponent<MovementComponent>();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
        movement.IsUsingUIinput = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
        movement.Crouch(false);
        movement.IsUsingUIinput = false;
    }

    void Update()
    {
        if (buttonPressed)
            movement.Crouch(true);
    }
}
