using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!UIManager.instance.MenuActive[1]) { UIManager.instance.uiScript.ToggleMenuUI(1); }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (UIManager.instance.MenuActive[1]) { UIManager.instance.uiScript.ToggleMenuUI(1); }
    }
}
