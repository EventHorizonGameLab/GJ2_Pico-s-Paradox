using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerV2 : MonoBehaviour, ITeleportable
{
    [Header("Player Parameters")]
    [SerializeField] float basePlayerSpeed;
    [SerializeField] float holdingSpeed;
    [SerializeField] float distancePerStep;
    float speed;
    [Header("DO NOT TOUCH")]
    [SerializeField] Transform targetMovePoint;
    [SerializeField] LayerMask obstacle;
    [SerializeField] LayerMask player;
    [SerializeField] GameObject interactIcon;
    [SerializeField] GameObject dialogueIcon;
    
    Vector3 movementVector;
    
    Rigidbody rb;
    
    

    //For holding objects
    bool blockX;
    bool blockZ;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Start()
    {
        targetMovePoint.parent = null;
        blockX = false;
        blockZ = false;
        speed = basePlayerSpeed;

    }
    private void FixedUpdate()
    {
        rb.MovePosition(targetMovePoint.position);
    }

    private void Update()
    {
        GameManager.playerIsOnTargertPoint = Vector3.Distance(transform.position, targetMovePoint.position) == 0;
        
        if (GameManager.isHoldingAnObject) { speed = holdingSpeed;  } else { speed = basePlayerSpeed; }

        movementVector = InputManager.Movement * distancePerStep;

        if (blockX) movementVector.x = 0;
        if (blockZ) movementVector.z = 0;
        if (GameManager.playerIsOnTargertPoint)
        {
            
            if (DirectionIsAvailable(movementVector) && GameManager.isColliding == false)
            {
                Vector3 newPosition = targetMovePoint.position + movementVector;

                targetMovePoint.position = newPosition;
            }
        }
        
        transform.position = Vector3.MoveTowards(transform.position, targetMovePoint.position, speed * Time.deltaTime);
    }
    private bool DirectionIsAvailable(Vector3 newInputVector)
    {
        if (blockX && Mathf.Abs(newInputVector.x) > 0) return true;
        if (blockZ && Mathf.Abs(newInputVector.z) > 0) return true;
        if (Mathf.Abs(newInputVector.x) > 0 && Mathf.Abs(newInputVector.z) > 0)
        {
            return false;
        }
        return true;
    }
    public void CheckAxisToHoldingObject(Vector3 direction)
    {
        if (GameManager.isHoldingAnObject)
        {
            if (direction.x != 0) { blockZ = true; }
            else if (direction.z != 0) { blockX = true; }
        }
        else
        {
            blockX = false;
            blockZ = false;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(targetMovePoint.position, 0.3f);
    }
        















        


    


        
            
        

     
}
