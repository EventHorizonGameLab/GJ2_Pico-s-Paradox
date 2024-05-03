using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class InputManager
{
    public static ActionMap ActionMap;

    public delegate void ButtonPressed();
    public static event ButtonPressed OnPause;

    static InputManager()
    {
        ActionMap = new ActionMap();
        ActionMap.Enable();
    }

    public static Vector3 Movement => ActionMap.Player.Movement.ReadValue<Vector3>(); // Vettore già normalizzato

    public static float HoldButtonPressed => ActionMap.Player.Hold.ReadValue<float>();

    public static float InteractButtonIsPressed => ActionMap.Player.Interact.ReadValue<float>();

    public static float PauseButtonPressed => ActionMap.UI.Pause.ReadValue<float>();

    public static bool IsMoving(out Vector3 direction) // Funzione utilizzabile in update per muovere il personaggio, restituisce l'input direzionale
    {
        direction = Movement;
        return direction != Vector3.zero;
    }

    public static bool IsTryingToInteract() // Funzione per controllare se il player interagisce
    {
        return InteractButtonIsPressed > 0;
    }
    /// <summary>
    /// activates the actionmapUI pog
    /// </summary>
    public static void UIInputsEnableDisabled(bool isActive)
    {
        if (isActive)
        {
            ActionMap.UI.Enable();

        }

        else
        {
            ActionMap.UI.Disable();
        }
    }

    public static void PlayerInputsEnableDisabled(bool isActive)
    {
        if (isActive)
        {
            ActionMap.Player.Enable();

        }

        else
        {
            ActionMap.Player.Disable();
        }
    }

    private static void OnPausePerformed(InputAction.CallbackContext context)
    {
        Debug.Log("ASDSFDF");
        OnPause?.Invoke();

    }

    public static void Initialize()
    {
        ActionMap = new ActionMap();
        ActionMap.Enable();
        ActionMap.UI.Pause.performed += OnPausePerformed;
    }
}
