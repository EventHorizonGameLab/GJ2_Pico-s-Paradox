using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    //Event
    public static Action<Vector3> OnBlockingAxis;

    [Header("Player Parameters")]
    [SerializeField] float basePlayerSpeed;
    [SerializeField] float holdingSpeed;
    float speed;
    [Header("DO NOT TOUCH")]
    [SerializeField] Transform targetMovePoint;
    [SerializeField] LayerMask obstacle;
    [SerializeField] LayerMask holdable;
    Vector3 movementVector;
    Vector3 lastTarget;
    //For holding objects
    bool blockX;
    bool blockZ;

    private void OnEnable()
    {
        GameManager.TemporaryOffInputs += BlockMovepoint;
    }

    private void Start()
    {
        targetMovePoint.parent = null;
        blockX = false;
        blockZ = false;
        speed = basePlayerSpeed;
    }

    private void Update()
    {
        float modX = Mathf.Abs(transform.position.x % 1);
        float modZ = Mathf.Abs(transform.position.z % 1);

        GameManager.playerIsOnTargertPoint = (Vector3.Distance(transform.position, targetMovePoint.position) == 0);
        GameManager.playerIsOnGrid = (modX == 0) && (modZ == 0);
        if(GameManager.isHoldingAnObject) { speed = holdingSpeed; } else { speed = basePlayerSpeed; }
        
        movementVector = InputManager.Movement;

        if (blockX) movementVector.x = 0;
        if (blockZ) movementVector.z = 0;
        
            transform.position = Vector3.MoveTowards(transform.position, targetMovePoint.position, speed * Time.deltaTime);
        
          
        

        if (GameManager.playerIsOnTargertPoint)
        {
            if (DirectionIsAvailable(movementVector))
            {
                Vector3 newPosition = targetMovePoint.position + movementVector; lastTarget = targetMovePoint.position;

                if (!GameManager.isHoldingAnObject && Physics.OverlapSphere(newPosition, 0.3f, obstacle | holdable).Length == 0)
                {
                    if(Physics.Raycast(transform.position,movementVector,0.5f,holdable))
                    {
                        transform.position = new(Mathf.Round(transform.position.x),transform.position.y, Mathf.Round(transform.position.z));
                        targetMovePoint.position = transform.position;
                    }
                    else
                        targetMovePoint.position = newPosition;

                }
                else if(GameManager.isHoldingAnObject && Physics.OverlapSphere(newPosition, 0.3f, obstacle ).Length == 0)
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

    void BlockMovepoint()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        float time = 0;
        float targetTime = 0.3f;
        while (time < targetTime) { targetMovePoint.position = lastTarget; targetTime -= Time.deltaTime; }
        yield return null;
    }

    







}
