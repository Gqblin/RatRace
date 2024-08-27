using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] GameObject[] itemPrefabs;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colliderEntity = collision.gameObject;
        Inventory colliderInv = colliderEntity.GetComponent<Inventory>();
        GameObject chosenPowerUp = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        ItemBehavior powerUpScript = chosenPowerUp.GetComponent<ItemBehavior>();

        switch (powerUpScript.itemType)
        {
            case ItemBehavior.ItemType.Consumable: StoreItem(colliderInv, powerUpScript);
                break;
            case ItemBehavior.ItemType.Impact: colliderInv.ItemActivation(powerUpScript);
                break;
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// Stores the ItemBehavior script of chosenItem in the 'storedItem' parameter of entityInventory.
    /// </summary>
    /// <param name="entityInventory">The targeted Inventory script to hold the ItemBehavior script.</param>
    /// <param name="chosenItem">The ItemBehavior script to be stored.</param>
    public void StoreItem(Inventory entityInventory, ItemBehavior chosenItem)
    {
        entityInventory.storedItem = chosenItem;
    }
}
