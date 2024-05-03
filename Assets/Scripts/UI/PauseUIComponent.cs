using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUIComponent : MonoBehaviour
{
    [SerializeField] RectTransform pausePanel;
    bool isPause;
    private void OnEnable()
    {
        InputManager.ActionMap.AlwaysOn.Pause.performed += ActivatePause;
    }


    private void OnDisable()
    {
        InputManager.ActionMap.AlwaysOn.Pause.performed -= ActivatePause;
    }
    private void ActivatePause(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log(isPause);
        isPause = !isPause;

        if (isPause)
        {
            GameManager.TimeScale(0);
            InputManager.ActionMap.Player.Disable();
        }

        else
        {
            GameManager.TimeScale(1);
            InputManager.ActionMap.Player.Enable();
        }

        pausePanel.gameObject.SetActive(isPause);
    }
}
