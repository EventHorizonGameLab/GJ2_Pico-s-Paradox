using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Furniture : MonoBehaviour, IHoldable
{
    
    
    [SerializeField] bool isInteractable; // Per debug
    [SerializeField] bool playerIsHolding; // per debug

    
    Vector3 correctPosition;
    
    

    private void OnEnable()
    {
        
    }
        
    public void InteractWithHoldable(Collider obj)
    {
        if(obj == null) return;
        if(isInteractable && playerIsHolding && GameManager.PlayerIsOnGrid) 
        {
            
            transform.parent = obj.transform;
            gameObject.layer = 0;
        }
    }
      



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
            InteractWithHoldable(collision);
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
        playerIsHolding = InputManager.HoldButtonPressed > 0;
        GameManager.IsHoldingAnObject = playerIsHolding;
        ImmediateRealeaseObject();
    }

   

    void ImmediateRealeaseObject() // Permette il rilascio immediato dell'oggetto e lo mantiene in griglia
    {
        if(!playerIsHolding) 
        {
            gameObject.layer = 7;
            transform.parent = null;
            
            correctPosition = new(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
            transform.position = correctPosition;

        }
    }


}
   

    




