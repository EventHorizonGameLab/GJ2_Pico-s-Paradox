using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    [SerializeField] public GameObject keySprite;
    [HideInInspector] public bool hasKey;
    bool isInteracting;

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
            if (hasKey)
            {
                return;
            }
            
            hasKey = true;
            keySprite.SetActive(true);
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
