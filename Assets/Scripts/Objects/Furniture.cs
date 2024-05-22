using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Furniture : MonoBehaviour, IInteractable
{

    [SerializeField] AudioData audioData;
    [SerializeField] bool isInteractable; // Serializzato per debug
    [SerializeField] bool isHolded; // Serializzato per debug
    AudioClip clip;
    bool readyForSound;

    
    Transform playerHolder;
    [SerializeField] bool tryingToHold;
    Vector3 lastPosition;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        clip = audioData.sfx_ObjectMoving;
    }

    private void OnEnable()
    {
        InputManager.ActionMap.Player.Hold.performed += HoldingBool;
        InputManager.ActionMap.Player.Movement.started += CanPlaySound;
        InputManager.ActionMap.Player.Movement.canceled += CanNotPlaySound;
    }

    private void OnDisable()
    {
        InputManager.ActionMap.Player.Hold.performed -= HoldingBool;
        InputManager.ActionMap.Player.Movement.started -= CanPlaySound;
        InputManager.ActionMap.Player.Movement.canceled -= CanNotPlaySound;
    }

    private void Start()
    {
        lastPosition = transform.position;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }




    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<IInteractor>(out _))
        {
            isInteractable = true;
        }
        else if (collision.gameObject.CompareTag("Spot")) GameManager.OnObjectInCorrectPos?.Invoke(true);
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.TryGetComponent<IInteractor>(out _))
        {


            if (tryingToHold)
            {
                isHolded = true;
                transform.parent = collision.transform;
                GameManager.isHoldingAnObject = true;
            }
            
            
            
            else
            {
                isHolded = false;
                transform.parent = null;
                GameManager.isHoldingAnObject = false;
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<IInteractor>(out _))
        {
            isInteractable = false;
        }
        else if (collision.gameObject.CompareTag("Spot")) GameManager.OnObjectInCorrectPos?.Invoke(false);
    }
    private void Update()
    {
        HoldingLogic();
        PlaySound();
    }
        
    void HoldingLogic()
    {
        if (isHolded && playerHolder != null)
        {
            transform.position = new Vector3(playerHolder.position.x, transform.position.y, playerHolder.position.z);
        }
    }
    void HoldingBool(InputAction.CallbackContext context)
    {
        if(isInteractable) { tryingToHold = !tryingToHold; }
        if(tryingToHold == false && transform.position != lastPosition) { GameManager.OnTemporaryOffInputs?.Invoke(0.2f); }
    }
    void PlaySound()
    {
        if (!GameManager.isHoldingAnObject) return;
        else if (GameManager.isHoldingAnObject && readyForSound ) { AudioManager.instance.PlayInLoop(clip); }
    }

    void CanPlaySound(InputAction.CallbackContext context) => readyForSound = true;
    void CanNotPlaySound(InputAction.CallbackContext context) => readyForSound = false;
                
                

    
    /////////////////////////// discarded code below //////////////////////////////////
    
    



        
        



    

   

    bool FurnitureIsOnGrid()
    {
        float modX = Mathf.Abs(transform.position.x % 1);
        float modZ = Mathf.Abs(transform.position.z % 1);

        
        bool isOnGrid = (modX == 0) && (modZ == 0);

        Debug.Log($"Checking grid position - X: {transform.position.x} ModX: {modX}, Z: {transform.position.z} ModZ: {modZ}, IsOnGrid: {isOnGrid}");
        return isOnGrid;
    }



    public void Interact()
    {
        // no need implementation
    }


}







