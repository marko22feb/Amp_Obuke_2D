using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueApplication : MonoBehaviour
{
    private Canvas Main_UI;
    private Canvas Options_UI;

    private void Awake()
    {
        Main_UI = GameObject.Find("Main_UI").GetComponent<Canvas>();
        Options_UI = GameObject.Find("Options_UI").GetComponent<Canvas>();
    }

    public void OnClick() {
        Time.timeScale = 1;
        Main_UI.enabled = true;
        Options_UI.enabled = false;
    }
}
