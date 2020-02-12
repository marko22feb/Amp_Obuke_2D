using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{

    private Transform tr;

    void Start()
    {
       tr = GetComponent<Transform>();
    }

 
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector2 newposition = new Vector2(tr.position.x + 0.01f, tr.position.y);
            tr.position = newposition;
            tr.rotation = new Quaternion(0, 0, 0, 0);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector2 newposition = new Vector2(tr.position.x - 0.01f, tr.position.y);
            tr.position = newposition;
            tr.rotation = new Quaternion(0, 180, 0, 0);
        }
    }
}
