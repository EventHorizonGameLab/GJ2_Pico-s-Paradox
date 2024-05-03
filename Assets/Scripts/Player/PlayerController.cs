using System;

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    //Event
    public static Action<Vector3> OnBlockingAxis;

    [Header("Player Parameters")]
    [SerializeField] float playerSpeed;
    [Header("DO NOT TOUCH")]
    [SerializeField] Transform targetMovePoint;
    [SerializeField] LayerMask obstacle;
    Vector3 movementVector;
    //For holding objects
    bool blockX;
    bool blockZ;

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void Start()
    {
        targetMovePoint.parent = null;
        blockX = false;
        blockZ = false;
    }

    private void Update()
    {
        GameManager.PlayerIsOnGrid = (Vector3.Distance(transform.position, targetMovePoint.position) == 0);

        movementVector = InputManager.Movement;

        if (blockX) movementVector.x = 0;
        if (blockZ) movementVector.z = 0;
        if(!Physics.Raycast(transform.position,movementVector,0.5f,obstacle))
            transform.position = Vector3.MoveTowards(transform.position, targetMovePoint.position, playerSpeed * Time.deltaTime);
        else
        {
            transform.position = new(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
            targetMovePoint.position = transform.position;
        }

        if (GameManager.PlayerIsOnGrid)
        {
            if (DirectionIsAvailable(movementVector))
            {
                Vector3 newPosition = targetMovePoint.position + movementVector;

                if (Physics.OverlapSphere(newPosition, 0.4f, obstacle).Length == 0)
                {
                    targetMovePoint.position = newPosition;
                }
            }
        }


    }

    private bool DirectionIsAvailable(Vector3 newInputVector)
    {
        if (blockX && Mathf.Abs(newInputVector.x) > 0) return true;
        if (blockZ && Mathf.Abs(newInputVector.z) > 0) return true;
        if (Mathf.Abs(newInputVector.x) > 0 && Mathf.Abs(newInputVector.z) > 0)
        {
            return false;
        }

        Vector3 xCheck = targetMovePoint.position + new Vector3(newInputVector.x, 0, 0);
        Vector3 zCheck = targetMovePoint.position + new Vector3(0, 0, newInputVector.z);

        bool xBlocked = Mathf.Abs(movementVector.x) > 0 && Physics.OverlapSphere(xCheck, 0.3f, obstacle).Length > 0;
        bool zBlocked = Mathf.Abs(movementVector.z) > 0 && Physics.OverlapSphere(zCheck, 0.3f, obstacle).Length > 0;

        return !xBlocked || !zBlocked;
    }

    public void CheckAxisToHoldingObject(Vector3 direction)
    {
        if (GameManager.IsHoldingAnObject)
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

    

    



}
