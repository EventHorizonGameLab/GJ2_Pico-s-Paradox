using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Parameters")]
    [SerializeField] float playerSpeed;
    [Header("DO NOT TOUCH")]
    [SerializeField] Transform targetMovePoint;
    [SerializeField] LayerMask obstacle;
    Vector3 movementVector;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        targetMovePoint.parent = null;
    }

    private void Update()
    {
        GameManager.PlayerIsOnGrid = (Vector3.Distance(transform.position, targetMovePoint.position) == 0);

        movementVector = InputManager.Movement;

        transform.position = Vector3.MoveTowards(transform.position, targetMovePoint.position, playerSpeed * Time.deltaTime);

        if(GameManager.PlayerIsOnGrid)
        {
            if(DirectionIsAvailable(movementVector))
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


}
