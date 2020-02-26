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
        //We only need output variable from ClosestPatrol(out int index). Here we are checking which object in the patrol list is closest to us, so we know from where to start the patrol.
        ClosestPatrol(out currentPatrol);
        rigidbody2d = GetComponent<Rigidbody2D>();

        //Arrays start from 0, List.Count gives total number of items in that list. Meaning that if we want the index of last item in that list, we have to use "List.Count -1".
        //In this case we are checking if our Current Patrol is greater then one item behind last. If we use if (currentPatrol == patrol.patrolPoints.Count -1) we would had gotten same result,
        //but I just wanted to explain a bit more how List.Count works.
        //This code means that if we are at last point in patrol we wish to return and move to other side, to the begining of the patrol.
        if (currentPatrol > patrol.patrolPoints.Count - 2) IsGoingRight = false;

        //If we are going right our next patrol is on our right, if we are not, then it's on the left. We are also rotating the enemy in the direction it's moving.
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
        //The code we wanted here was moved to new functions and functions are called from here. Just to keep the code clean.
        DecideNextMove();
        Move();
    }

    private void DecideNextMove()
    {
        //We create a new local variable (these variables only exists inside of these functions) distance and set it to distance from us to the patrol point we are moving towards.
        float distance;
        distance = Vector2.Distance(transform.position, patrol.patrolPoints[nextPatrol].transform.position);

        //If distance is less then 25 centimetes we are setting our Current patrol point to be the point we reached.
        if (distance < 0.25f)
        {
            currentPatrol = nextPatrol;
        }

        //If we are at end of our patrol we set IsGoingRight to false. If we are at begining of our patrol we set IsGoingRight to true. We also rotate character here.
        if (currentPatrol == patrol.patrolPoints.Count - 1) { IsGoingRight = false; transform.rotation = new Quaternion(0, 0, 0, 0); }
        if (currentPatrol == 0){ IsGoingRight = true; transform.rotation = new Quaternion(0, 180, 0, 0);
    }
        if (IsGoingRight) {
            nextPatrol = currentPatrol + 1;
        } else nextPatrol = currentPatrol - 1;
    }

    private void Move()
    {
        //Set the velocity to positive or negative depending of direction we wish to move.
        if (IsGoingRight) rigidbody2d.velocity = new Vector2(Speed, rigidbody2d.velocity.y);
        else rigidbody2d.velocity = new Vector2(-Speed, rigidbody2d.velocity.y);
    }


    //Functions can have inputs and outputs. Inputs are what's used inside of the function, the data we will need in order to finish it, and output is something we will need outside of the functions.
    //For example public int SummOfTwoNumbers (int a, int b, out int c) will the give summ of two integers we put in.
    public GameObject ClosestPatrol(out int index)
    {
        //We set the gameobject to be null just in case we forgot to add patrol points.
        GameObject closest = null;
        //Foreach loop doesn't have an index, so you have to manually count it.
        int arrayIndex = 0;
        //This is the Index at which the closest object is.
        int position = 0;

        //We create float distance and we set it's value to be infinity. So that any distance to any object is considered smaller.
        float distance = float.PositiveInfinity;

        //For each Gameobject "NAME"(I used "ob" but you can use anything here, it doesn't matter most of the times) in patrol(script we made named PatrolRoute).patrolPoints(List of GameObjects) we check distance between us and it.
        //If distance between us is smaller then float distance then we set this gameobject as closest to us and we set up new distance so we can check if next object have smaller distance then one we already set.
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
