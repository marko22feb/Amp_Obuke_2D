using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<StatComponent>() != null)
        {
            collision.gameObject.GetComponent<StatComponent>().ModifyHealthBy(-9999);
        }
    }
}
