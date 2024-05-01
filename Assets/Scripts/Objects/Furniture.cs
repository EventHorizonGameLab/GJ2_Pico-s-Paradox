using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Furniture : MonoBehaviour, IHoldable
{
    
    
    [SerializeField] bool isInteractable; // Per debug
    
    

    private void OnEnable()
    {
        
    }
        
    public void InteractWithHoldable(Collider obj)
    {
        
        if(isInteractable && InputManager.HoldButtonPressed > 0) 
        {
            transform.parent = obj.transform;
            gameObject.layer = 0;
            GameManager.OnPlayerHoldingObject?.Invoke(true);
        }
        else
        {
            if (GameManager.PlayerIsOnGrid)
            {
                transform.parent = null;
                gameObject.layer = 7;
                GameManager.OnPlayerHoldingObject?.Invoke(false);
            }
        }
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
   

    




