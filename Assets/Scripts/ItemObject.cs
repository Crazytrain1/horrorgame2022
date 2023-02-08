using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;
    [SerializeField] AudioClip audioClip;
    
    public void OnPickupItem()
    {
        
        AudioSource.PlayClipAtPoint(audioClip, this.transform.position);    
        InventorySystem.current.Add(referenceItem);
        Destroy(gameObject);
    }
}
