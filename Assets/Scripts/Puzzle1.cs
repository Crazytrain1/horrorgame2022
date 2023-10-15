using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    [SerializeField] List<GameObject> Child;
    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < Child.Count; i++)
        {
            if (!Child[i].GetComponent<Pencil>().correct)
            {
                break;
            }
            else if (i== Child.Count - 1)
            {
                Debug.Log("Puzzle solve baby!!");
            }

        }
    }
}
