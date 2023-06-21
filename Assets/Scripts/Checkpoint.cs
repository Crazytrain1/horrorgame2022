using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Checkpoint : MonoBehaviour
{
    [SerializeField] int level;
    [SerializeField] int checkpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // big epic gamer moment ghoul
            Debug.Log("Checkpoint" + checkpoint + "reached");
            GameManager.Instance.saveInventory();
            GameManager.Instance.saveLevel(level, checkpoint);
            Destroy(gameObject);
        }
    }
}
