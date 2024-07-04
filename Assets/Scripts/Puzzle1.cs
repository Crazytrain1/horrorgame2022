using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    [SerializeField] List<GameObject> Child;
    [SerializeField] GameObject item;

    bool notSolved = true;

    void Update()
    {

        if (notSolved)
        {
            for (int i = 0; i < Child.Count; i++)
            {
                if (!Child[i].GetComponent<Pencil>().correct)
                {
                    break;
                }
                else if (i == Child.Count - 1)
                {
                    item.SetActive(true);
                    notSolved= false;
                    break;
                }

            }
        }
    }
}
