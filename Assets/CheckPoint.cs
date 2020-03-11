using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool SaveOnceAlready = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player" & !SaveOnceAlready)
        {
            GameController.Control.SaveGame();
            SaveOnceAlready = true;
        }
    }
}
