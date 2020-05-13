using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingComponent : MonoBehaviour
{
    public Direction dir;
    public Axis axis;
    public float speed;
    public float currentValue = 0;

    private void Update()
    {
        currentValue += speed * Time.deltaTime;
        float changedValue = 0;

        if (currentValue > 360) currentValue = 0;
        if (currentValue < 0) currentValue = 360;

        switch (dir)
        {
            case Direction.right:
                changedValue = currentValue;
                break;
            case Direction.left:
                changedValue = -currentValue;
                break;
            case Direction.up:
                changedValue = currentValue;
                break;
            case Direction.down:
                changedValue = -currentValue;
                break;
            default:
                break;
        }
       
        switch (axis)
        {
            case Axis.x:
                transform.rotation = Quaternion.Euler(changedValue, transform.rotation.y, transform.rotation.z);
                break;
            case Axis.y:
                transform.rotation = Quaternion.Euler(transform.rotation.x, changedValue, transform.rotation.z);
                break;
            case Axis.z:
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, changedValue);
                break;
            default:
                break;
        }
    }
}

public enum Direction {right, left, up, down}
public enum Axis {x,y,z}
