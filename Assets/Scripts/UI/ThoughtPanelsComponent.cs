using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtPanelsComponent : MonoBehaviour
{
    [SerializeField] GameObject dialogueCanvas;
    bool isInteracting;
    public GameObject textObject;
    public GameObject imageObjectChanger;

    TextMeshProUGUI text;
    Image image;
    int dialogueCounter;
    int lastLineCounter = 999;
    int lineCounter => dialogues[dialogueCounter].lineCounter;
    Lines currentLine => dialogues[dialogueCounter].lines[lineCounter];

    public Dialogue[] dialogues;

    private void Start()
    {
        text = textObject.GetComponent<TextMeshProUGUI>();
        image = imageObjectChanger.GetComponent<Image>();
        Debug.Log(text.text);
    }
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

        
        if (dialogues[dialogueCounter].lineCounter < dialogues[dialogueCounter].lines.Length)
        {
            Debug.Log("last line = " + lastLineCounter + "\nline counter = " + lineCounter);
            if (lineCounter != lastLineCounter)
            {
                InputManager.ActionMap.Player.Movement.Disable();
                text.text = currentLine.text;
                image.sprite = currentLine.Ted.sprite;
                image.color = currentLine.Ted.color;
                lastLineCounter = lineCounter;
                dialogueCanvas.SetActive(true);
            }
            else
            {
                dialogues[dialogueCounter].lineCounter++;
                OnInteraction(context);
            }
        }
        else if (dialogueCounter < dialogues.Length)
        {
            InputManager.ActionMap.Player.Movement.Enable();
            dialogueCounter++;
            dialogueCanvas.SetActive(false);
            lastLineCounter = 999;
        }
        else
        {
            InputManager.ActionMap.Player.Movement.Enable();
            dialogueCounter = 0;
            lastLineCounter = 999;
            dialogueCanvas.SetActive(false);
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

[System.Serializable]
public class Lines
{
    public string text;

    [Tooltip("the sprite of the one saying this line")]
    public Image Ted; //https://static.wikia.nocookie.net/eallafinearrivamamma/images/a/a3/Ted_profile.jpg/revision/latest?cb=20120810080952&path-prefix=it
    //its an inside joke im sorry if you read it venice or marco <3
    [HideInInspector] public bool hasBeenRead;
}

[System.Serializable]
public class Dialogue
{
    public Lines[] lines;
    [HideInInspector] public int lineCounter;

}