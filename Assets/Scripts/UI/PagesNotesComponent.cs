using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PagesNotesComponent : MonoBehaviour
{
    Image pagePanel; //immagine dell'oggetto in scena
    bool isReading;
    [SerializeField] GameObject lastPagePanel;
    bool isInteracting;
    [SerializeField] Image newPage; //immagine che sostituisce quella in scena
    [SerializeField] bool isLast;
    public static Action lastPageBubble;
    public static bool isPanelActive;
    private void Awake()
    {
        pagePanel = lastPagePanel.GetComponent<Image>();
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
        //Debug.Log("hi interacting = " + isInteracting);
        if (isInteracting == false)
        {
            return;
        }

        if (isReading == false)
        {
            if (isLast)
            {
                isPanelActive = true;
            }
            //Debug.Log("gogogoogog " + isReading);
            isReading = true;
            InputManager.ActionMap.Player.Movement.Disable();
            pagePanel.sprite = newPage.sprite;
            pagePanel.color = newPage.color;
            lastPagePanel.transform.parent.gameObject.SetActive(true);
            //pagePanel.rectTransform.parent.gameObject.SetActive(true);
        }

        else
        {

            Debug.Log("no " + isReading);
            isReading = false;
            InputManager.ActionMap.Player.Movement.Enable();
            lastPagePanel.transform.parent.gameObject.SetActive(false);
            if (isLast)
            {
                InputManager.ActionMap.Player.Interact.started -= OnInteraction;
                lastPageBubble?.Invoke();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IInteractor>(out _))
        {
            isInteracting = true;
        }
        //else
        //{
        //    isInteracting = false;
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IInteractor>(out _))
        {
            isInteracting = false;
        }
    }
}