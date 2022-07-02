using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryUI : MonoBehaviour
{
    public GameObject m_slotPrefab;
    public GameObject InventoryBar;
    void Start()
    {
        InventorySystem.current.onInventoryChangedEvent += OnUpdateInventory;
        InventoryBar.SetActive(false);
    }
    private void OnUpdateInventory()
    {
        InventoryBar.SetActive(true);
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        DrawInventory();
    }
    public void DrawInventory()
    {

        foreach (InventorySystem.InventoryItem item in InventorySystem.current.inventory)
        {

            AddInventorySlot(item);
        }
    }

    public void AddInventorySlot(InventorySystem.InventoryItem item)
    {
        InventoryBar.SetActive(true);
        GameObject obj = Instantiate(m_slotPrefab);
        obj.transform.SetParent(transform, false);

        SlotScript slot = obj.GetComponent<SlotScript>();
        slot.Set(item);

    }
}