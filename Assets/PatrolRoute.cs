using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PatrolRoute : MonoBehaviour
{
    public List<GameObject> patrolPoints;

#if UNITY_EDITOR
    void Update()
    {
        patrolPoints.Clear();

        foreach(Transform child in transform)
        {
            patrolPoints.Add(child.gameObject);
        }
    }
#endif

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < patrolPoints.Count -1; i++)
        {
            Gizmos.DrawLine(patrolPoints[i].transform.position, patrolPoints[i + 1].transform.position);
                }
    }
}
