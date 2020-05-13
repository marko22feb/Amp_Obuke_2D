using UnityEngine;
using System.Collections;

public class PathFinderComponent : MonoBehaviour
{
    [SerializeField]
    Transform target = null;

    void Start()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        Vector3 p = target.position;

        float gravity = Physics.gravity.magnitude;

        Vector2 planarTarget = new Vector3(p.x, p.y);
        Vector2 planarPostion = new Vector3(transform.position.x, transform.position.y);

        float distance = Vector2.Distance(planarTarget, planarPostion);

        rigid.velocity = new Vector2(3, Mathf.Sqrt(-2.0f * Physics2D.gravity.y * distance));//finalVelocity;
    }
}
