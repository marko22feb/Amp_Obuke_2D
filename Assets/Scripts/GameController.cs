using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Control;
    public int GemAmount;
    private Text gemText;

    private void Awake()
    {
        if (Control == null)
        {
            Control = this;
        }
        else if (Control != this)
        {
            Destroy(this.gameObject);
        }

        gemText = GameObject.Find("GemText").GetComponent<Text>();
    }

    private void Start()
    {
        UpdateGemText();
    }

    public void UpdateGemText()
    {
        gemText.text = "GEMS: " + GemAmount;
    }
}
