using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool isInteracting;
    [SerializeField] Animator anim;
    [SerializeField] Key key;

    private void OnEnable()
    {
        InputManager.ActionMap.Player.Interact.started += OnInteraction;
    }
    private void OnDisable()
    {
        InputManager.ActionMap.Player.Interact.started -= OnInteraction;
    }

    private void OnInteraction(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (isInteracting == false)
        {
            return;
        }

        else
        {
            if (key.hasKey) 
            {

                anim.SetBool("isOpeningDoor", true);
                key.hasKey = false;
                key.keySprite.SetActive(false);
            }

            else
            {
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IInteractor>(out _))
        {
            isInteracting = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IInteractor>(out _))
        {
            isInteracting = false;
        }
    }
}
