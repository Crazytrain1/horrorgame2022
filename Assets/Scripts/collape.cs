using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collape : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject ground;
    [SerializeField] GameManager shake;
    void Start()
    {
         
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // big epic gamer moment collapse
            gameObject.GetComponent<ScreenShake>().Crumbling(0.15f, 0.4f);
            ground.SetActive(false);
        }
    }
}
