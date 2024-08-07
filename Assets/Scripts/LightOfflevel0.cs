using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOfflevel0 : MonoBehaviour
{
    [SerializeField] eventCollider objectCollider;
    [SerializeField] GameObject InteractDisplayObject;
    [SerializeField] AudioClip glassBreaking;
    [SerializeField] AudioSource sound;
    private InteractDisplay _InteractDisplay;
    private void Awake()
    {
        objectCollider.lightOff += lightOff;
    }

    private void Start()
    {
        _InteractDisplay = InteractDisplayObject.GetComponent<InteractDisplay>();
    }

    private void OnDestroy()
    {
        objectCollider.lightOff -= lightOff;
    }

    private void lightOff()
    {
        objectCollider.lightOff -= lightOff;
        this.GetComponent<Animation>().Play("lightOffLevel0");
        StartCoroutine("Flashlight");

    } 
    //display feedback to force the player to use the flashlight
    IEnumerator Flashlight()
    {
        
        yield return new WaitForSeconds(2);
        _InteractDisplay.SetInteractDisplay("[F]", "It's pretty dark. I should use my flashlight", null);
        yield return new WaitForSeconds(4);
        _InteractDisplay.UpdateInteractDisplay();

        yield return null;
    }

    private void glassBreak()
    {
        sound.PlayOneShot(glassBreaking);
    }
}
