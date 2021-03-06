using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InventorySystem : MonoBehaviour
{
    public static InventorySystem current;
    private Dictionary<InventoryItemData, InventoryItem> m_itemDictionary;
    public List<InventoryItem> inventory;


    public event Action onInventoryChangedEvent;
    public void InventoryChangedEvent()
    {
        if (onInventoryChangedEvent != null)
        {
            onInventoryChangedEvent();
        }
    }

    private void Awake()
    {
        current = this;
        inventory = new List<InventoryItem>();
        m_itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();

    }

    public void Add(InventoryItemData referenceData)
    {
        if( m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.AddToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(referenceData);
            inventory.Add(newItem);
            m_itemDictionary.Add(referenceData, newItem);
        }
        current.InventoryChangedEvent();
    }


    public void Remove(InventoryItemData referenceData)
    {
        if (m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.RemoveFromStack();
            if(value.stackSize==0)
            {
                inventory.Remove(value);
                m_itemDictionary.Remove(referenceData);
            }
        }
        current.InventoryChangedEvent();
    }

    public InventoryItem Get(InventoryItemData referenceData)
    {
        if(m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            return value;
        }
        return null;
    }

    [Serializable]
    public class InventoryItem
    {
        public InventoryItemData data;
        public int stackSize;

        public InventoryItem(InventoryItemData source)
        {
            data = source;
            AddToStack();
        }
        public void AddToStack()
        {
            stackSize++;
        }
        public void RemoveFromStack()
        {
            stackSize--;
        }



    }
}
