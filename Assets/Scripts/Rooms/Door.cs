using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool isInteracting;
    [SerializeField] Animator anim;
    [SerializeField] Key key;
    [SerializeField] bool isFirstDoor;
    [SerializeField] AudioData audioData;
    bool isLocked = true;

    private void OnEnable()
    {
        InputManager.ActionMap.Player.Interact.started += OnInteraction;
        if (isFirstDoor)
        {
            Room5.gustavo += OpenFirstDoor;
        }
    }
    private void OnDisable()
    {
        InputManager.ActionMap.Player.Interact.started -= OnInteraction;
        if (isFirstDoor)
        {
            Room5.gustavo -= OpenFirstDoor;
        }
    }

    private void OpenFirstDoor()
    {
        isLocked = false;
    }

    private void OnInteraction(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (isInteracting == false)
        {
            return;
        }

        else if (isFirstDoor)
        {
            if (isLocked == false)
            {
                anim.SetBool("isOpeningDoor", true);
            }
        }
        else
        {
            if (key.hasKey)
            {
                AudioManager.instance.PlaySFX(audioData.sfx_openingDoor);
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