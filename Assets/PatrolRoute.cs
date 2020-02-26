using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will be executable in Edit mode as well. (Before Play is pressed) This is useful during map building, and testing out features.
[ExecuteInEditMode]
public class PatrolRoute : MonoBehaviour
{
    //List of child objects under this GameObject. Lists are a part of "using System.Collections.Generic;"
    public List<GameObject> patrolPoints;


//Since we don't want this part to be executed in Play mode as well, we are making an if statement where we check if we are currently running in editor.
//Update in editor isn't executed every frame but rather each time a change is made to transfrom of the object.
#if UNITY_EDITOR
    void Update()
    {
        //We must first clear the list before adding more objects to it. Otherwise we get a list of duplicate objects.
        patrolPoints.Clear();

        //foreach Transform compomenent named child in transform add the gameobject(owner) of that component to the list.
        foreach(Transform child in transform)
        {
            patrolPoints.Add(child.gameObject);
        }
    }
#endif

    //This is one of Unity built functions which is executed in editor, so no need to specify that like above. 
    //Gizmos are visual representations inside of editor that can be scripted. These are not visibile inside of the Play mode and are just developers tools.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;


        //Draw the line from each gameobject in the list.
        for (int i = 0; i < patrolPoints.Count -1; i++)
        {
            Gizmos.DrawLine(patrolPoints[i].transform.position, patrolPoints[i + 1].transform.position);
                }
    }
}
