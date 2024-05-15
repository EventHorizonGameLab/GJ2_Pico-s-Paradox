using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartRoomText : MonoBehaviour
{
    [SerializeField] GameObject dialogueCanvas;
    bool isInteracting;
    public GameObject textObject;
    public GameObject imageObjectChanger;

    TextMeshProUGUI text;
    Image image;
    int dialogueCounter;
    int lastLineCounter = 999;
    int lineCounter { get => dialogues[dialogueCounter].lineCounter; set => dialogues[dialogueCounter].lineCounter = value; }
    Lines currentLine => dialogues[dialogueCounter].lines[lineCounter];

    public Dialogue[] dialogues;

    private void Start()
    {
        text = textObject.GetComponent<TextMeshProUGUI>();
        image = imageObjectChanger.GetComponent<Image>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggereeeeeeeeeeed");
        ShowDialogue();
    }

    private void OnEnable()
    {
        InputManager.ActionMap.Player.Interact.started += HideDialogue;
    }

    private void HideDialogue(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        dialogueCanvas.SetActive(false);
        InputManager.ActionMap.Player.Enable();
    }

    private void OnDisable()
    {
        InputManager.ActionMap.Player.Interact.started -= HideDialogue;
    }

    private void ShowDialogue()
    {
      
        if (dialogues[dialogueCounter].lineCounter < dialogues[dialogueCounter].lines.Length)
        {
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
                lineCounter++;
                ShowDialogue();
            }
        }
        //else if (dialogueCounter < dialogues.Length)
        //{
        //    InputManager.ActionMap.Player.Enable();
        //    lineCounter = 0;
        //    Debug.Log(dialogues[dialogueCounter].lineCounter + "\n" + lineCounter);
        //    dialogueCounter++;
        //    dialogueCanvas.SetActive(false);
        //    lastLineCounter = 999;

        //    if (dialogueCounter >= dialogues.Length)
        //    {
        //        InputManager.ActionMap.Player.Enable();
        //        dialogueCounter = 0;
        //        lastLineCounter = 999;
        //        dialogueCanvas.SetActive(false);
        //    }
        //}

    }
}
