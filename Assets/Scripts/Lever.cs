using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Sprite On;
    public Sprite Off;

    private SpriteRenderer sr;
    public Door door;

    public bool CanPullLever;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (door.Locked)
            sr.sprite = Off;
        else sr.sprite = On;
          
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CanPullLever = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (CanPullLever)
            {
                if (collision.gameObject.GetComponent<Animator>().GetBool("IsCrouching"))
                {
                    door.Locked = !door.Locked;
                    CanPullLever = false;
                    if (door.Locked)
                        sr.sprite = Off;
                    else sr.sprite = On;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CanPullLever = true;
    }
}
