using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public int NeededGems = 10;
    public bool Locked = true;
    public Canvas Main_UI;
    public Text infoText;
    public Text spawnedText;

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
                else {
                    if (spawnedText == null)
                    {
                        SpawnText("Not enough gems. You are missing: " + (NeededGems - GameController.Control.GemAmount));
                    }
                }
            } else
            {
                if (spawnedText == null)
                {
                    SpawnText("Door is Locked");
                }
            }
        }
    }

    public void SpawnText (string desc)
    {
        spawnedText = Instantiate(infoText, Main_UI.transform);
        spawnedText.text = desc;
        StartCoroutine(DestroyText());
    }

    IEnumerator DestroyText()
    {
        yield return new WaitForSeconds(3f);
        Destroy(spawnedText.gameObject);
        spawnedText = null;
        StopCoroutine(DestroyText());
    }
}
