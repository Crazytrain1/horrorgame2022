using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particuleOpti1 : MonoBehaviour
{
    [SerializeField] List<GameObject> particulesDeactivate;
    [SerializeField] List<GameObject> particulesActivate;
    [SerializeField] GameObject otherParticuleOpti;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (GameObject particule in particulesDeactivate)
            {
                particule.SetActive(false);
            }
            foreach (GameObject particule in particulesActivate)
            {
                particule.SetActive(true);
            }
            if(otherParticuleOpti!= null)
            {
                otherParticuleOpti.SetActive(true);
            }
            
            this.gameObject.SetActive(false);
        }
    }
}
