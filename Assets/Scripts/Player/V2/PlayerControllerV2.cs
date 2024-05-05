using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerV2 : MonoBehaviour
{
    

    [Header("Player Parameters")]
    [SerializeField] float basePlayerSpeed;
    [SerializeField] float holdingSpeed;
    float speed;
    [Header("DO NOT TOUCH")]
    [SerializeField] Transform targetMovePoint;
    [SerializeField] LayerMask obstacle;
    [SerializeField] LayerMask holdable;
    [SerializeField] LayerMask player;
    [SerializeField] bool rayHitObj;
    Vector3 movementVector;
    public float rayLenght;
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
        float modX = Mathf.Abs(transform.position.x % 1);
        float modZ = Mathf.Abs(transform.position.z % 1);

        GameManager.playerIsOnTargertPoint = (Vector3.Distance(transform.position, targetMovePoint.position) == 0);
        GameManager.playerIsOnGrid = (modX == 0) && (modZ == 0);
        if (GameManager.isHoldingAnObject) { speed = holdingSpeed; rayLenght = 0; } else { speed = basePlayerSpeed; rayLenght = 0.51f; }

        movementVector = InputManager.Movement * 0.1f;

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

        //Vector3 xCheck = targetMovePoint.position + new Vector3(newInputVector.x, 0, 0);
        //Vector3 zCheck = targetMovePoint.position + new Vector3(0, 0, newInputVector.z);

        //bool xBlocked = Mathf.Abs(movementVector.x) > 0 && Physics.OverlapSphere(xCheck, 0.3f, obstacle).Length > 0;
        //bool zBlocked = Mathf.Abs(movementVector.z) > 0 && Physics.OverlapSphere(zCheck, 0.3f, obstacle).Length > 0;

        //return !xBlocked || !zBlocked;

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
