using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using Cinemachine;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Spawner;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject LookAt;
    [SerializeField] GameObject parent;
    [SerializeField] Transform parentT;
    [SerializeField] enum Level {MainMenu,Level0,Level1,Level2,Level3, Level4};
    [SerializeField] Level NextLevel;
    

    void Start()

    {
        
        parent = Instantiate(Player as GameObject);
        
        parent.transform.position = Spawner.transform.position;
        LookAt.transform.position = parent.transform.position;
        //
        parentT = parent.transform;
        LookAt.transform.SetParent(parentT, true);
        
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player")
        {
            SceneManager.LoadScene((int)NextLevel);
        }

    }
}
