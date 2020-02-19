using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnJumpTrigger : MonoBehaviour
{
    private RaycastHit2D raycasthit2D;
    private BoxCollider2D boxcollider2D;
    private Vector3 offset;
    [SerializeField] private LayerMask playerLayerMask = default;

    void Start()
    {
        boxcollider2D = GetComponent<BoxCollider2D>();
        offset = new Vector3(0, 0.5f, 0);
    }

    void Update()
    {
        raycasthit2D = Physics2D.BoxCast(boxcollider2D.bounds.center - offset, boxcollider2D.bounds.size, 0f, Vector2.down, 3f, playerLayerMask);
        if (raycasthit2D.collider != null)
        {
            boxcollider2D.isTrigger = true;
        }
        else
        {
            boxcollider2D.isTrigger = false;
        }
    }
}
