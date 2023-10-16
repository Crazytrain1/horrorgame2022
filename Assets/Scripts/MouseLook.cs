using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] Transform playerBody;

    [SerializeField] ScreenShake screenShake;

    private bool _canLook = true;
    float xRotation = 0f;
    private void Awake()
    {
        GameManager.StateChanged += GameManager_StateChanged;

        GameManager.screenShake += ScreenShake;

        GameManager_StateChanged(GameManager.Instance.StartingState);
    }
    private void OnDestroy()
    {
        GameManager.StateChanged -= GameManager_StateChanged;

        GameManager.screenShake -= ScreenShake;
    }

    private void ScreenShake(float duration, float magnitude)
    {

        StartCoroutine(Shake(duration,magnitude));
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;
            
        while (elapsed < duration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
    private void GameManager_StateChanged(GameManager.GameState State)
    {
        _canLook = State == GameManager.GameState.Playing;
        Debug.Log(_canLook);
        Debug.Log(State.ToString());
        if (_canLook || State == GameManager.GameState.cinematic)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (_canLook)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
