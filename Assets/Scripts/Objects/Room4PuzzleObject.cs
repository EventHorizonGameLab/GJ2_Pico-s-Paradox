using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room4PuzzleObject : MonoBehaviour, IInteractable
{
    

    [SerializeField] InteractionPuzzle puzzleController;
    [SerializeField] GameObject icon;
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
        icon.SetActive(false);
    }
    public void Interact()
    {
        puzzleController.InteractWithObject(this.gameObject);
        isInteractable = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<IInteractor>(out _) && isInteractable) { icon.SetActive(true); } else { icon.SetActive(false); }
        if (other.TryGetComponent<IInteractor>(out _) && InputManager.IsTryingToInteract() && isInteractable)
        {
            Interact();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IInteractor>(out _)) { icon.SetActive(false); }
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
