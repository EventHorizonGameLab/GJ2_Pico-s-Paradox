using System;
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
    int lineCounter { get => dialogues[dialogueCounter].lineCounter; set => dialogues[dialogueCounter].lineCounter = value; }
    Lines currentLine => dialogues[dialogueCounter].lines[lineCounter];

    public Dialogue[] dialogues;

    [SerializeField] bool isTriggered;

    RoomTrigger roomTrigger;

    [SerializeField] bool isLastBubble;

    bool isFirstTime = true;

    private void Awake()
    {
        roomTrigger = gameObject.GetComponent<RoomTrigger>();

    }
    private void Start()
    {
        text = textObject.GetComponent<TextMeshProUGUI>();
        image = imageObjectChanger.GetComponent<Image>();
    }
    private void OnEnable()
    {
        if (isLastBubble)
        {
            PagesNotesComponent.lastPageBubble += ShowLastMessage;
        }
        else if (isTriggered == false)
        {
            InputManager.ActionMap.Player.Interact.started += OnInteraction;
        }

        else
        {
            roomTrigger.OnDialogue += ShowDialogue;
            InputManager.ActionMap.Player.Interact.started += HideDialogue;
        }
    }

    private void ShowLastMessage()
    {

        ShowDialogue();
        InputManager.ActionMap.Player.Interact.started += OnInteraction;
        PagesNotesComponent.lastPageBubble -= ShowLastMessage;
    }

    private void HideDialogue(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        dialogueCanvas.SetActive(false);
        InputManager.ActionMap.Player.Enable();
    }

    private void OnDisable()
    {
        if (isTriggered == false)
        {
            InputManager.ActionMap.Player.Interact.started -= OnInteraction;
        }
        else
        {
            roomTrigger.OnDialogue -= ShowDialogue;
            InputManager.ActionMap.Player.Interact.started -= HideDialogue;
        }
    }

    void HideLastBubble()
    {
        InputManager.ActionMap.Player.Enable();
        dialogueCanvas.SetActive(false);
        Room5.gustavo?.Invoke();
        InputManager.ActionMap.Player.Interact.started -= OnInteraction;
    }

    private void OnInteraction(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (isInteracting == false)
        {
            return;
        }
        ShowDialogue();
    }
    private void ShowDialogue()
    {

        //Debug.LogWarning("showdialogue");
        if (dialogues[dialogueCounter].lineCounter < dialogues[dialogueCounter].lines.Length)
        {

            Debug.Log("last line = " + lastLineCounter + "\nline counter = " + lineCounter);
            if (lineCounter != lastLineCounter)
            {
                Debug.LogWarning("second if");
                InputManager.ActionMap.Player.Movement.Disable();
                text.text = currentLine.text;
                image.sprite = currentLine.Ted.sprite;
                image.color = currentLine.Ted.color;
                lastLineCounter = lineCounter;
                dialogueCanvas.SetActive(true);

            }
            else
            {
                Debug.Log("TUO PADRE");
                lineCounter++;
                ShowDialogue();
            }
        }
        else if (dialogueCounter < dialogues.Length)
        {
            InputManager.ActionMap.Player.Enable();
            lineCounter = 0;
            //Debug.Log(dialogues[dialogueCounter].lineCounter + "\n" + lineCounter);
            dialogueCounter++;
            dialogueCanvas.SetActive(false);
            lastLineCounter = 999;

            if (dialogueCounter >= dialogues.Length)
            {
                if (isLastBubble)
                {
                    HideLastBubble();
                }

                else if (isTriggered == false)
                {
                    //Debug.Log("TUA MAMMA");

                    InputManager.ActionMap.Player.Enable();
                    dialogueCounter = 0;
                    lastLineCounter = 999;
                    dialogueCanvas.SetActive(false);
                }
                else
                {
                    //Debug.Log("TUA MAMMA 2.0");
                    InputManager.ActionMap.Player.Enable();
                    dialogueCanvas.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered)
        {
            return;
        }
        if (other.TryGetComponent<IInteractor>(out _))
        {
            isInteracting = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isTriggered)
        {
            return;
        }
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