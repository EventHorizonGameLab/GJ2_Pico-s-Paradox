using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room4PuzzleObject : MonoBehaviour, IInteractable
{
    

    [SerializeField] InteractionPuzzle puzzleController;
    public bool isInteractable;

    private void OnEnable()
    {
        puzzleController.OnWrongInteraction += ResetPuzzle;
    }

    private void OnDisable()
    {
        puzzleController.OnWrongInteraction += ResetPuzzle;
    }

    private void Start()
    {
        isInteractable = true;
    }
    public void Interact()
    {
        puzzleController.InteractWithObject(this.gameObject);
        isInteractable = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<IHolder>(out _) && InputManager.IsTryingToInteract() && isInteractable)
        {
            Interact();
        }
    }

    void ResetPuzzle()
    {
        StartCoroutine(DelayReset());
    }

    IEnumerator DelayReset()
    {
        yield return new WaitForSeconds(0.1f);
        isInteractable = true;
    }



}
