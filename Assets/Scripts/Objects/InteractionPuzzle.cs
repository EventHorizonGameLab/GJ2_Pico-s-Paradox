using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPuzzle : MonoBehaviour, IInteractable
{
    //Events
    public Action OnWrongInteraction;

    [SerializeField] List<GameObject> correctOrder = new();
    [SerializeField] AudioData audioData;
    
    int interactionIndex;

    

    private void Start()
    {
        interactionIndex = 0;
    }

    public void InteractWithObject( GameObject obj)
    {
        if(obj == correctOrder[interactionIndex])
        {
            Debug.Log("Hai interagito con l'oggetto numero:" +  interactionIndex );
            interactionIndex++;
            
            if (interactionIndex >= correctOrder.Count) 
            {
                AudioManager.instance.PlaySFX(audioData.sfx_puzzleSolved);
                GameManager.OnKeyObtained?.Invoke();
                correctOrder.Clear();
                Debug.Log("Puzzle Risolto");
            }
        }
        else
        {
            AudioManager.instance.PlaySFX(audioData.sfx_puzzleWrong);
            interactionIndex = 0;
            OnWrongInteraction();
            Debug.Log("devi ricominciare");
        }
    }

    public void Interact()
    {
        
    }
}
