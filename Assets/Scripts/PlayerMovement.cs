using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 2f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] bool isGrounded;
    [SerializeField] GameObject SpotLight;

    [SerializeField] AudioSource _lampSoundOpen;
    [SerializeField] AudioSource _lampSoundClose;
    [SerializeField] AudioSource _footStep = default;
    [SerializeField] private AudioClip[] woodClips = default;
    [SerializeField] private AudioClip[] catacombsClips = default;
    [SerializeField] private AudioClip[] cementClips = default;


    [SerializeField] bool _useFootSteps = true;

    private float footstepTimer = 0;   
    private bool canMove = true;
    private bool FlashlightOpen = false;
    Vector3 velocity;
    public InventoryItemData referenceItem;

    [SerializeField] private float baseStepSpeed = 0.5f;
    
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
       _useFootSteps = State == GameManager.GameState.Playing;
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

            Vector3 move = (transform.right * x + transform.forward * z).normalized;

            controller.Move(move * speed * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);



            if (Input.GetKeyDown(KeyCode.F) && (InventorySystem.current.Get(referenceItem) != null))
            {

                if (FlashlightOpen)
                {
                    _lampSoundOpen.Play();
                    SpotLight.SetActive(false);
                    FlashlightOpen = false;
                }
                else
                {
                    _lampSoundClose.Play();
                    SpotLight.SetActive(true);
                    FlashlightOpen = true;
                }
            }
        }

        if (_useFootSteps)
        {
            
            HandleFootStep();
        }
        if (Input.GetKeyDown(KeyCode.Escape)&& GameManager.Instance.State != GameManager.GameState.Pausing && GameManager.Instance.State == GameManager.GameState.Playing)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Pausing);
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.State == GameManager.GameState.Pausing)
        {
            GameManager.Instance.UpdateGameState( GameManager.Instance.PreviousState);
        }

        

    }

    private void HandleFootStep()
    {
        if (!isGrounded ) return;        
        if ((Input.GetAxis("Vertical") == 0) && (Input.GetAxis("Horizontal") == 0)) return;
        footstepTimer -= Time.deltaTime;

        if(footstepTimer <= 0) 
        {
            if(Physics.Raycast((this.transform.position + new Vector3(0,1,0)), Vector3.down, out RaycastHit hit, 3))
            {
               switch (hit.collider.tag) 
                {
                    case "Footsteps/WOOD":                       
                        _footStep.PlayOneShot(woodClips[UnityEngine.Random.Range(0, woodClips.Length-1)]);
                        break;
                    case "Footsteps/CEMENT":
                        //need to implement  sound!!
                        break;
                    case "Footsteps/CATACOMBS":
                        //need to implement  sound!!
                        break;

                    default:
                        _footStep.PlayOneShot(woodClips[UnityEngine.Random.Range(0, woodClips.Length - 1)]);
                        break;

                }

                footstepTimer = baseStepSpeed;
            }
        }
    }
}
