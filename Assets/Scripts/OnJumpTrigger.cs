using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnJumpTrigger : MonoBehaviour
{
    private RaycastHit2D raycasthit2D;
    private BoxCollider2D boxcollider2D;
    private Vector3 offset;
    [SerializeField] private LayerMask playerLayerMask = default;
    [SerializeField] private LayerMask boardLayerMask = default;

    public List<GameObject> Neighbours;
    public bool Detected = false;

    void Start()
    {
        boxcollider2D = GetComponent<BoxCollider2D>();
        offset = new Vector3(0, 0.5f, 0);
        StartCoroutine(Delay(1f));
    }

    void Update()
    {
        raycasthit2D = Physics2D.BoxCast(boxcollider2D.bounds.center - offset, boxcollider2D.bounds.size, 0f, Vector2.down, 3f, playerLayerMask);
        bool CanChangeValue = true;
        if (raycasthit2D.collider != null)
        {
            Detected = true;
            if (!boxcollider2D.isTrigger)
                ChangeTriggerValue(true);
        }
        else
        {
            Detected = false;
            if (boxcollider2D.isTrigger)
            {
                foreach (GameObject gob in Neighbours)
                {
                    if (gob.GetComponent<OnJumpTrigger>().Detected == true) CanChangeValue = false;
                }
                if (CanChangeValue)
                {
                    ChangeTriggerValue(false);
                }
            }
        }
    }

    public void SetTrigger(bool set)
    {
        if (boxcollider2D.isTrigger != set)
        {
            boxcollider2D.isTrigger = set;
        }
    }

    public void ChangeTriggerValue(bool set)
    {
        SetTrigger(set);
        foreach (GameObject gob in Neighbours)
        {
            gob.GetComponent<OnJumpTrigger>().SetTrigger(set);
        }
    }

    void GetNeighbours()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(boxcollider2D.bounds.center, boxcollider2D.bounds.size, 0f, boardLayerMask);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == "Board")
            {
                bool found = false;
                foreach (GameObject gob in Neighbours)
                {
                    if (gob == hitColliders[i].gameObject)
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    Neighbours.Add(hitColliders[i].gameObject);
                }
            }
        }
    }

    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        GetNeighbours();
        StopCoroutine(Delay(.1f));
    }
}
