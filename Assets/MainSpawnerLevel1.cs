using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpawnerLevel1 : MonoBehaviour
{
    [SerializeField] GameObject Player;
    void Start()
    {
        Player.transform.position = this.transform.position;
    }


}
