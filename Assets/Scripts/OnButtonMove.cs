using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnButtonMove : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public MovementComponent movement;
    public bool RightButton = false;
    public bool ButtonPressed = false;
    private float distance;
    private float str = 1f;
    private float duration;

    private RectTransform rt;
    private Vector2 startingPosition;
    private Vector2 endPosition;

    private void Awake()
    {
        movement = GameObject.Find("Player").GetComponent<MovementComponent>();
        rt = GetComponent<RectTransform>();
        distance = transform.parent.GetComponent<RectTransform>().rect.width / 2;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {

        duration = 0f;
        startingPosition = eventData.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        duration += Time.deltaTime;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        endPosition = eventData.position;
        distance = Vector2.Distance(startingPosition, endPosition);
        movement.rigidbody2d.velocity = new Vector2(str * duration * distance, movement.rigidbody2d.velocity.y);
        Debug.Log(str * duration * distance);
    }
}
