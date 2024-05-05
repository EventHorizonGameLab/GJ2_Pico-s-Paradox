using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class PlayerRayCaster : MonoBehaviour
{
    
    [SerializeField] LayerMask layerToIgnore;
    float rayLenght;
    bool isColliding;
   


    private void Start()
    {
        rayLenght = 0.6f;
    }
    private void Update()
    {
        if (GameManager.isHoldingAnObject) { rayLenght = 0; } else { rayLenght = 0.6f; }
        if (InputManager.Movement != Vector3.zero)
        {
            isColliding = CollisionCheck(InputManager.Movement);
            GameManager.isColliding = isColliding;
        }
    }
    bool CollisionCheck(Vector3 direction)
    {
        Vector3 leftRayOrigin = transform.position - Vector3.Cross(direction, Vector3.up) * 0.5f + Vector3.up * 0.5f;
        Vector3 rightRayOrigin = transform.position + Vector3.Cross(direction, Vector3.up) * 0.5f + Vector3.up * 0.5f;

        bool hitCenter = Physics.Raycast(transform.position + Vector3.up * 0.5f, direction, rayLenght, ~layerToIgnore);
        bool hitLeft = Physics.Raycast(leftRayOrigin, direction, rayLenght, ~layerToIgnore);
        bool hitRight = Physics.Raycast(rightRayOrigin, direction, rayLenght, ~layerToIgnore);

        return  hitCenter || hitLeft || hitRight;
    }
        
        
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Vector3 direction = InputManager.Movement.normalized;
        Vector3 leftRayOrigin = transform.position - Vector3.Cross(direction, Vector3.up) * 0.5f + Vector3.up * 0.5f;
        Vector3 rightRayOrigin = transform.position + Vector3.Cross(direction, Vector3.up) * 0.5f + Vector3.up * 0.5f;

        Gizmos.DrawRay(leftRayOrigin, direction * 0.5f);
        Gizmos.DrawRay(rightRayOrigin, direction * 0.5f);
        Gizmos.DrawWireSphere(leftRayOrigin, 0.1f);
        Gizmos.DrawWireSphere(rightRayOrigin, 0.1f);
        Gizmos.DrawRay(leftRayOrigin, Vector3.right * 0.5f);
    }
            
            
            

        
        

        
        

        


        
        
        

        

        

}
