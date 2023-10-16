using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public void Crumbling(float duration, float magnitude)
    {
        GameManager.Instance.Shake(3f, 0.2f);
    }
}
