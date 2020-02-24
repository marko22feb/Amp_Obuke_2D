using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public PatrolRoute patrol;
    public float Speed = 5;

    private int nextPatrol;
    private int currentPatrol;

    private Rigidbody2D rigidbody2d;

    bool IsGoingRight = true;

    void Start()
    {
        GameObject gob = ClosestPatrol(out currentPatrol);
        rigidbody2d = GetComponent<Rigidbody2D>();

        if (currentPatrol > patrol.patrolPoints.Count - 2) IsGoingRight = false;
        if (IsGoingRight)
        {
            nextPatrol = currentPatrol + 1;
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            nextPatrol = currentPatrol - 1;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    void Update()
    {
        DecideNextMove();
        Move();
    }

    private void Move()
    {
        if (IsGoingRight) rigidbody2d.velocity = new Vector2(Speed, rigidbody2d.velocity.y);
        else rigidbody2d.velocity = new Vector2(-Speed, rigidbody2d.velocity.y);
    }

    private void DecideNextMove()
    {
        float distance;
        distance = Vector2.Distance(transform.position, patrol.patrolPoints[nextPatrol].transform.position);
        if (distance < 0.25f)
        {
            currentPatrol = nextPatrol;
        }

        if (currentPatrol == patrol.patrolPoints.Count - 1) { IsGoingRight = false; transform.rotation = new Quaternion(0, 0, 0, 0); }
        if (currentPatrol == 0){ IsGoingRight = true; transform.rotation = new Quaternion(0, 180, 0, 0);
    }
        if (IsGoingRight) {
            nextPatrol = currentPatrol + 1;
        } else nextPatrol = currentPatrol - 1;

        Debug.Log(nextPatrol);
    }

    public GameObject ClosestPatrol(out int index)
    {
        GameObject closest = null;
        int arrayIndex = 0;
        int position = 0;

        float distance = float.PositiveInfinity;

        foreach(GameObject ob in patrol.patrolPoints)
        {
            if (Vector2.Distance(transform.position, ob.transform.position) < distance) {
                closest = ob;
                distance = Vector2.Distance(transform.position, ob.transform.position);
                position = arrayIndex;
            }
            arrayIndex++;
        }
        index = position;
        return closest;
    }
}
