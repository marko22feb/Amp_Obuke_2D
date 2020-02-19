using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform tr;
    public Transform PlayerTr;
  

    private void Start()
    {
        tr = GetComponent<Transform>();
    }
    void Update()
    {
        tr.position = new Vector3(PlayerTr.position.x, PlayerTr.position.y, -10);
        
    }
}
