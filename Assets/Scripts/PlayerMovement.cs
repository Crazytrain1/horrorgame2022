using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;


enum Stance
{
    Standing, crouching, crawling
}
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    [SerializeField] BoxCollider hitBoxStanding;
    [SerializeField] BoxCollider hitBoxCrouching;
    [SerializeField] BoxCollider hitBoxCrawling;

    [SerializeField] GameObject cameraPlayer;
    [SerializeField] GameObject flashlight;
    [SerializeField] GameObject CanStandObject;

    [SerializeField] float speed = 2f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] bool isGrounded;
    [SerializeField] GameObject SpotLight;

    [SerializeField] AudioSource _lampSoundOpen;
    [SerializeField] AudioSource _lampSoundClose;
    [SerializeField] AudioSource _footStep;
    [SerializeField] private AudioClip[] woodClips = default;
    [SerializeField] private AudioClip[] catacombsClips = default;
    [SerializeField] private AudioClip[] cementClips = default;
    [SerializeField] private AudioClip[] metalClips = default;



    [SerializeField] bool _useFootSteps = true;
    [SerializeField] private float baseStepSpeed = 0.5f;


    
    private float footstepTimer = 0;   
    private bool canMove = false;
    private bool FlashlightOpen = false;


    private Vector3 crawling = new Vector3(0, (float)-1.45, 0) ;
    private Vector3 standing = new Vector3(0, 0, 0);
    private Vector3 crouching = new Vector3(0, (float)-0.72, 0);
    Vector3 velocity;
    public InventoryItemData referenceItem;


    private Stance current;
    
    
    private void Awake()
    {
        GameManager.StateChanged += GameManager_StateChanged;
        current = Stance.Standing;

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

            switch (current)
            {

                case Stance.Standing:
                    Standing();
                    break;
                case Stance.crouching:
                    Crouching();
                    break;
                case Stance.crawling:
                    Crawling();
                    break;
                    
            }
            UnityEngine.Debug.Log(current);

            if (current == Stance.Standing)
            {
                UnityEngine.Debug.Log("Here in standing");
                if(Input.GetKeyDown(KeyCode.C))
                {
                    current = Stance.crouching;
                }
                if(Input.GetKeyDown(KeyCode.LeftControl)) {
                    current = Stance.crawling;
                }

            }

            else if(current == Stance.crouching)
            {
                UnityEngine.Debug.Log("Here in crouching");
                if (Input.GetKeyDown(KeyCode.C) && CanStand())
                {
                    current = Stance.Standing;
                }
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    current = Stance.crawling;
                }
            }

            else if (current == Stance.crawling)
            {
                UnityEngine.Debug.Log("Here in crawling");
                if (Input.GetKeyDown(KeyCode.C) && CanCrouch())
                {
                    current = Stance.crouching;
                }
                if (Input.GetKeyDown(KeyCode.LeftControl) && CanStand())
                {                   
                    current = Stance.Standing;    
                }
            }
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = ((transform.right * x) + (transform.forward * z));

            if(move.magnitude > 1 || move.magnitude < -1)
            {
                move.Normalize();
            }

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
        

        if (_useFootSteps)
        {
            
            HandleFootStep();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.State != GameManager.GameState.Pausing && GameManager.Instance.State == GameManager.GameState.Playing)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Pausing);
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.State == GameManager.GameState.Pausing)
        {
            GameManager.Instance.UpdateGameState( GameManager.Instance.PreviousState);
        }
    }   


    }

    private void Standing()
    {
        //camera lerp to new position
        hitBoxStanding.enabled = true;
        hitBoxCrawling.enabled = false;
        hitBoxCrouching.enabled = false;

        controller.height = 1.7F;
        controller.center = new Vector3(0, 0.93F, 0);
        controller.radius = 0.46F;
        cameraPlayer.GetComponent<Camera>().nearClipPlane = 0.5F; 
        StartCoroutine(LerpCamera(cameraPlayer.transform.localPosition, standing));
        speed = 2f;
    }
    private void Crouching()
    {
        //camera lerp to new position
        hitBoxCrouching.enabled = true;
        hitBoxCrawling.enabled = false;
        hitBoxStanding.enabled = false;

        controller.center = new Vector3(0, 0.65F, 0);
        controller.height = 1.0F;
        controller.radius = 0.46F;
        cameraPlayer.GetComponent<Camera>().nearClipPlane = 0.01F;
        StartCoroutine(LerpCamera(cameraPlayer.transform.localPosition,crouching));

        speed = 1.5f;
    }

    private void Crawling()
    {
        //camera lerp to new position
        hitBoxCrawling.enabled = true;
        hitBoxCrouching.enabled = false;
        hitBoxStanding.enabled = false;

        controller.center = new Vector3(0, 0.32F, 0);
        controller.radius = 0.20F;
        controller.height = 0.5F;
        cameraPlayer.GetComponent<Camera>().nearClipPlane = 0.01F;
        StartCoroutine(LerpCamera(cameraPlayer.transform.localPosition, crawling));
        speed = 1f;



    }

    private bool CanStand()
    {
        RaycastHit Hit;
        if (Physics.Raycast(CanStandObject.transform.position, CanStandObject.transform.TransformDirection(Vector3.up), out Hit,  Mathf.Infinity) && Hit.distance < 1.40)
        {

            UnityEngine.Debug.DrawRay(CanStandObject.transform.position, CanStandObject.transform.TransformDirection(Vector3.up) * Hit.distance, Color.yellow);
            UnityEngine.Debug.Log(Hit.distance);
            
            return false;
        }
        else
        {
            UnityEngine.Debug.DrawRay(CanStandObject.transform.position, CanStandObject.transform.TransformDirection(Vector3.up) * 1000, Color.white);
            return true;
        }
    }

    private bool CanCrouch()
    {
        RaycastHit Hit;
        if (Physics.Raycast(CanStandObject.transform.position, CanStandObject.transform.TransformDirection(Vector3.up), out Hit, Mathf.Infinity) && Hit.distance < 0.65)
        {

            UnityEngine.Debug.DrawRay(CanStandObject.transform.position, CanStandObject.transform.TransformDirection(Vector3.up) * Hit.distance, Color.yellow);
            UnityEngine.Debug.Log(Hit.distance);

            return false;
        }
        else
        {
            UnityEngine.Debug.DrawRay(CanStandObject.transform.position, CanStandObject.transform.TransformDirection(Vector3.up) * 1000, Color.white);
            return true;
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
                        _footStep.PlayOneShot(woodClips[UnityEngine.Random.Range(0, woodClips.Length - 1)]);
                        break;
                    case "Footsteps/CATACOMBS":
                        _footStep.PlayOneShot(catacombsClips[UnityEngine.Random.Range(0, catacombsClips.Length - 1)]);
                        break;
                    case "Footsteps/METAL":
                        //need to implement sound !!
                        _footStep.PlayOneShot(metalClips[UnityEngine.Random.Range(0, metalClips.Length - 1)]);
                        break;

                    default:
                        _footStep.PlayOneShot(woodClips[UnityEngine.Random.Range(0, woodClips.Length - 1)]);
                        break;

                }

                footstepTimer = baseStepSpeed;
            }
        }
    }

    IEnumerator LerpCamera(Vector3 startingPosition,Vector3 finalPosition)
    {

     
            cameraPlayer.transform.localPosition = Vector3.Lerp(startingPosition, finalPosition , Time.deltaTime*2);
            yield return null;


    }
}
