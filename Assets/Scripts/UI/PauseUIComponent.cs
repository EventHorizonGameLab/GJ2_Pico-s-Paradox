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
        InputManager.OnPause += ActivatePause;
    }
    private void OnDisable()
    {
        InputManager.OnPause -= ActivatePause;
    }
    private void ActivatePause()
    {
        Debug.Log(isPause);
        isPause = !isPause;

        if (isPause)
        {
            Time.timeScale = 0f;
            InputManager.PlayerInputsEnableDisabled(false);
            InputManager.UIInputsEnableDisabled(true);
        }

        else
        {
            Time.timeScale = 1f;
            InputManager.PlayerInputsEnableDisabled(true);
            InputManager.UIInputsEnableDisabled(false);
        }

        pausePanel.gameObject.SetActive(isPause);
    }
}
