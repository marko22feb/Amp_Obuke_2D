using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public int NeededGems = 10;
    public bool Locked = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!Locked)
            {
                if (GameController.Control.GemAmount >= NeededGems)
                {
                    SceneManager.LoadScene("SampleScene");
                }
            }
        }
    }
}
