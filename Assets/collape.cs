using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collape : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject ground;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // big epic gamer moment collapse
            ground.SetActive(false);
        }
    }
}
