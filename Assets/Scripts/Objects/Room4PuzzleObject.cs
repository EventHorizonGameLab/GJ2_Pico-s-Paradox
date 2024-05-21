using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room4PuzzleObject : MonoBehaviour, IInteractable
{
    

    [SerializeField] InteractionPuzzle puzzleController;
    [SerializeField] AudioData audioData;
    AudioClip clip;
    
    public bool isInteractable;

    private void Awake()
    {
        clip = audioData.sfx_interactSound;
    }

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
        AudioManager.instance.PlaySFX(clip);
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.TryGetComponent<IInteractor>(out _) && InputManager.IsTryingToInteract() && isInteractable)
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
