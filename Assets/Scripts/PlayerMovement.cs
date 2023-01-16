using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 5f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] bool isGrounded;
    [SerializeField] GameObject SpotLight;
    private bool canMove = true;
    private bool FlashlightOpen = false;
    Vector3 velocity;
    public InventoryItemData referenceItem;
    
    private void Awake()
    {
        GameManager.StateChanged += GameManager_StateChanged;

    }

    private void OnDestroy()
    {
        GameManager.StateChanged -= GameManager_StateChanged;
    }

    private void GameManager_StateChanged(GameManager.GameState State)
    {
        canMove = State == GameManager.GameState.Playing;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (canMove)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Escape)&& GameManager.Instance.State != GameManager.GameState.Pausing)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Pausing);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.State == GameManager.GameState.Pausing)
        {
            GameManager.Instance.UpdateGameState( GameManager.Instance.PreviousState);
        }
        if(Input.GetKeyDown(KeyCode.F) && (InventorySystem.current.Get(referenceItem) != null))
        {
                if (FlashlightOpen)
                {
                    SpotLight.SetActive(false);
                FlashlightOpen = false;
                }
                else
                {
                    SpotLight.SetActive(true);
                FlashlightOpen= true;
                }
        }

    }

    }
