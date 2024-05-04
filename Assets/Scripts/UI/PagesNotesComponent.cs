using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PagesNotesComponent : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject pagePanel;
    bool isReading = true;
    GameObject lastPagePanel;
    bool isInteracting;
    public void Interact()
    {
        if (isReading)
        {
            MovementDisabled();
            lastPagePanel = Instantiate(pagePanel);
        }
        else
        {
            MovementEnabled();
            Destroy(lastPagePanel);
        }
        isReading = !isReading;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.TryGetComponent<IHolder>(out var uwu))
    //    {
    //        if (InputManager.ActionMap.Player.Interact.WasPerformedThisFrame())
    //        {
    //            Interact();
    //        }
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{

    //    if (other.TryGetComponent<IHolder>(out var uwu))
    //    {
    //        if (InputManager.ActionMap.Player.Interact.WasPerformedThisFrame())
    //        {
    //            Debug.Log("uwu");
    //            Interact();
    //        }
    //    }
    //}

    private void MovementDisabled()
    {
        InputManager.ActionMap.Player.Movement.Disable();
    }

    private void MovementEnabled()
    {
        InputManager.ActionMap.Player.Movement.Enable();
    }

}
