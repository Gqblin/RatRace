using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ItemBehavior storedItem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && storedItem != null) 
        { 
            ItemActivation(storedItem);
            storedItem = null;
        }
    }

    public void ItemActivation(ItemBehavior itemToActivate)
    {
        StartCoroutine(itemToActivate.ItemEffect(gameObject));
    }
}
