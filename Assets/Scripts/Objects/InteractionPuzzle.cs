using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPuzzle : MonoBehaviour
{
    //Events
    public Action OnWrongInteraction;

    [SerializeField] List<GameObject> correctOrder = new();

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
            //TODO:play interaction sound
            if (interactionIndex >= correctOrder.Count) 
            {
            //TODO: play clear puzzle sound
            //TODO: obtain key && show in HUD
            correctOrder.Clear();
            Debug.Log("Puzzle Risolto");
            }
        }
        else
        {
            //TODO: play wrong order sound
            interactionIndex = 0;
            OnWrongInteraction();
            Debug.Log("devi ricominciare");
        }
    }

    
}
