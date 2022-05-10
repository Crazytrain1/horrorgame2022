using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
  public InventoryItemData referenceItem;

    public void OnPickupItem()
    {
        Debug.Log("3 quart");
        InventorySystem.current.Add(referenceItem);
        Destroy(gameObject);
        Debug.Log("fonctionne");
    }
}
