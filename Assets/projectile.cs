using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float x;
    public float y;
    public Rigidbody2D r2D;

    private void Start()
    {
        r2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        r2D.velocity = new Vector2(x, y);
    }
}
