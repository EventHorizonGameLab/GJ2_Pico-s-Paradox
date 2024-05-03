using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Furniture : MonoBehaviour, IHoldable
{
    
    
    [SerializeField] bool isInteractable; // Serializzato per debug
    [SerializeField] bool playerIsHolding; // Serializzato per debug


    private Vector3 correctPosition;
    private bool wasHoldingLastFrame; // Traccia lo stato del frame precedente

    private void Update()
    {
        bool currentlyHolding = InputManager.HoldButtonPressed > 0;
        if (currentlyHolding != playerIsHolding)
        {
            playerIsHolding = currentlyHolding;
            HandleHoldingChange(playerIsHolding);
        }
        GameManager.IsHoldingAnObject = playerIsHolding;
        if (!playerIsHolding)
        {
            ImmediateReleaseObject();
        }
        wasHoldingLastFrame = playerIsHolding;
    }

    private void HandleHoldingChange(bool isHolding)
    {
        if (!isHolding && wasHoldingLastFrame)
        {
            // Cambio di layer avviene non appena il giocatore rilascia l'oggetto
            gameObject.layer = 7;
        }
    }

    public void InteractWithHoldable(Collider obj)
    {
        if (obj == null || !isInteractable || !playerIsHolding || !GameManager.PlayerIsOnGrid)
            return;

        transform.parent = obj.transform;
        gameObject.layer = 0;
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

    private void ImmediateReleaseObject()
    {
        // Reimposta il parent a null e corregge la posizione sull'albero più vicino
        transform.parent = null;
        correctPosition = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
        transform.position = correctPosition;
    }


}
   

    




