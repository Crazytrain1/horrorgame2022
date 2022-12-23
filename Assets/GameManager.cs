using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using Cinemachine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    [SerializeField] GameObject Spawner;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject parent;
    [SerializeField] Transform parentT;
    [SerializeField] enum Level {MainMenu,Level0,Level1,Level2,Level3, Level4};
    [SerializeField] Level NextLevel;
    private void Awake()
    {
        Instance = this;
    }

    void Start()

    {
        
        parent = Instantiate(Player as GameObject);
        
        parent.transform.position = Spawner.transform.position;

        
        parentT = parent.transform;
      
        
        
    }

}
