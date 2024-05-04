using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIComponent : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    bool isPause;
    [SerializeField] GameObject showControlPanel;
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
            InputManager.SwitchToUI();
        }

        else
        {
            GameManager.TimeScale(1);
            InputManager.SwitchToPlayer();
        }

        pausePanel.SetActive(isPause);
    }

    public void OnReturnToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void OnCLoseShowControl()
    {
        showControlPanel.SetActive(false);
    }
}
