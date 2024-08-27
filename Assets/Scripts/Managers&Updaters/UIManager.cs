 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public UIUpdater uiScript;
    public GameObject[] UIMenus;
    public bool[] MenuActive;

    private void Awake()
    {
        if (this != instance && instance != null) { Destroy(this); }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
