using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpawnerLevel1 : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject parent;
    void Start()
    {
        parent = Instantiate(Player as GameObject);

        parent.transform.position = this.transform.position;
        Debug.Log(parent.transform.position);
    }


}
