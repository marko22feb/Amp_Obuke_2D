using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHideIfAndroid : MonoBehaviour
{
    private void Start()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            this.gameObject.SetActive(false);
        }
    }
}
