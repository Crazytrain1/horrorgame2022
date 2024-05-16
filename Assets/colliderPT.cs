using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class colliderPT : MonoBehaviour



{

    [SerializeField] GameObject currentBranch;
    [SerializeField] GameObject newBranch;

    Vector3 leftLowerPoint;
    private Vector3 rawposition = new Vector3((float)-0.46, (float)-0.36, (float)-1.01);


    private void Start()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        Bounds bounds = boxCollider.bounds;

        leftLowerPoint = new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);
        Debug.Log("World Space Left Lower Point: " + leftLowerPoint);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 newPos = other.transform.position - leftLowerPoint;

            other.transform.position = new Vector3(newPos.x + rawposition.x, newPos.y + rawposition.y, newPos.z + rawposition.z);
            newBranch.SetActive(true);
            currentBranch.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
