using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;

    public enum ItemType
    {
        Consumable = 0,
        Impact = 1
    }
    public ItemType itemType;

    public virtual IEnumerator ItemEffect(GameObject user)
    {
        Debug.Log("Coroutine has Ended!");
        yield return null;
    }
}
