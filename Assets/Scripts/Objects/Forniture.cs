using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Forniture : MonoBehaviour, IHoldable
{
    
    public bool isHolded;
    public bool isInteractable;
    
    

    private void OnEnable()
    {
        isHolded = false;
    }
        
    public void InteractWithHoldable(Collider obj)
    {
        
        if(isInteractable && InputManager.HoldButtonPressed > 0) 
        {
            transform.parent = obj.transform;
            
        }
        else
            transform.parent = null;
    }



    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<IHolder>(out _)) { }
        {
            isInteractable = true;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.TryGetComponent<IHolder>(out _)) { }
        {
            InteractWithHoldable(collision);
        }

    }





    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<IHolder>(out _)) { }
        {
            isInteractable = false;
        }
    }
        

}
   

    




