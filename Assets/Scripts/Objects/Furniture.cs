using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Furniture : MonoBehaviour
{


    [SerializeField] bool isInteractable; // Serializzato per debug
    [SerializeField] bool playerIsHolding; // Serializzato per debug
    [SerializeField] bool isHolded; // Serializzato per debug

    Vector3 correctPosition;
    Transform playerHolder;



    




    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<IHolder>(out _))
        {
            isInteractable = true;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.TryGetComponent<IHolder>(out _))
        {
            if (InputManager.HoldButtonPressed != 0)
            {
                isHolded = true;
                playerHolder = collision.gameObject.transform;
            }
            else if (!FurnitureIsOnGrid())
            {
                isHolded = true;
            }
            else
            {
                isHolded = false;
                playerHolder = null;
            }
        }
    }
                
                





    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<IHolder>(out _))
        {
            isInteractable = false;
        }
    }

    private void Update()
    {
        playerIsHolding = InputManager.HoldButtonPressed != 0;
        GameManager.isHoldingAnObject = isHolded;
        
        HoldingLogic();
    }



    

   

    bool FurnitureIsOnGrid()
    {
        float modX = Mathf.Abs(transform.position.x % 1);
        float modZ = Mathf.Abs(transform.position.z % 1);

        
        bool isOnGrid = (modX == 0) && (modZ == 0);

        Debug.Log($"Checking grid position - X: {transform.position.x} ModX: {modX}, Z: {transform.position.z} ModZ: {modZ}, IsOnGrid: {isOnGrid}");
        return isOnGrid;
    }

    void HoldingLogic()
    {
        if (isHolded && playerHolder != null)
        {
            transform.position = new Vector3(playerHolder.position.x, transform.position.y, playerHolder.position.z);
        }
    }
    
}
   

    




