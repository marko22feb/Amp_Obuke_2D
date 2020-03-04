using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemPickUp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameController.Control.GemAmount++;
            GameController.Control.UpdateGemText();
            Destroy(this.gameObject);
        }
    }
}
