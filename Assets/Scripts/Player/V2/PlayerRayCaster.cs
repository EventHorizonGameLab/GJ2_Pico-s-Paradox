using System;
using Color = UnityEngine.Color;
using System.Drawing;
using UnityEngine;


public class PlayerRayCaster : MonoBehaviour
{
    public static Action<GameObject> OnObjectHeld;

    [SerializeField] LayerMask layerToIgnore;
    float rayLenght;
    bool isColliding;

    GameObject heldObj;

    private void OnEnable()
    {
        OnObjectHeld += GetObjectHeld;
    }
    private void OnDisable()
    {
        OnObjectHeld -= GetObjectHeld;
    }

    private void Start()
    {
        rayLenght = 0.65f;
    }
    private void Update()
    {
        
        if (InputManager.Movement != Vector3.zero)
        {
            isColliding = CollisionCheck(InputManager.Movement) || ObjectCollision(heldObj, InputManager.Movement);
            GameManager.isColliding = isColliding;
        }
    }
    bool CollisionCheck(Vector3 direction)
    {
        Vector3 leftRayOrigin = transform.position - Vector3.Cross(direction, Vector3.up) * 0.45f + Vector3.up * 0.5f;
        Vector3 rightRayOrigin = transform.position + Vector3.Cross(direction, Vector3.up) * 0.45f + Vector3.up * 0.5f;

        bool hitCenter = Physics.Raycast(transform.position + Vector3.up * 0.5f, direction, rayLenght, ~layerToIgnore);
        bool hitLeft = Physics.Raycast(leftRayOrigin, direction, rayLenght, ~layerToIgnore);
        bool hitRight = Physics.Raycast(rightRayOrigin, direction, rayLenght, ~layerToIgnore);

        return  hitCenter || hitLeft || hitRight;
    }

    void GetObjectHeld(GameObject obj)
    {
        
        heldObj = obj;
    }

    bool ObjectCollision(GameObject obj,Vector3 direction)
    {
        if(obj == null) return false;
        Collider collider = obj.GetComponent<Collider>();
        Vector3 bounds = collider.bounds.extents;
        float distance = direction.x > direction.z ? bounds.x : bounds.z;
        distance += 0.2f;

        bool isHitting = Physics.Raycast(obj.transform.position, direction, distance, ~layerToIgnore);
        Debug.DrawRay(obj.transform.position, direction * distance ,Color.green);
        return isHitting;
    }


        
        
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Vector3 direction = InputManager.Movement;
        Vector3 leftRayOrigin = transform.position - Vector3.Cross(direction, Vector3.up) * 0.45f + Vector3.up * 0.5f;
        Vector3 rightRayOrigin = transform.position + Vector3.Cross(direction, Vector3.up) * 0.45f + Vector3.up * 0.5f;

        Gizmos.DrawRay(leftRayOrigin, direction * 0.5f);
        Gizmos.DrawRay(rightRayOrigin, direction * 0.5f);
        Gizmos.DrawRay(transform.position + Vector3.up * 0.5f, direction * 0.5f);
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.5f, 0.1f);
        Gizmos.DrawWireSphere(leftRayOrigin, 0.1f);
        Gizmos.DrawWireSphere(rightRayOrigin, 0.1f);
    }
            
            
            

        
        

        
        

        


        
        
        

        

        

}
