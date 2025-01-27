using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class InputManager
{
    public static ActionMap ActionMap;

    static InputManager()
    {
        ActionMap = new ActionMap();
        ActionMap.Enable();
        //SwitchToUIInput();
    }

    public static Vector3 Movement => ActionMap.Player.Movement.ReadValue<Vector3>(); // Vettore gi� normalizzato

    public static float HoldButtonPressed => ActionMap.Player.Hold.ReadValue<float>();

    public static float InteractButtonIsPressed => ActionMap.Player.Interact.ReadValue<float>();

    public static bool IsMoving(out Vector3 direction) // Funzione utilizzabile in update per muovere il personaggio, restituisce l'input direzionale
    {
        direction = Movement;
        return direction != Vector3.zero;
    }

    public static bool IsTryingToInteract() // Funzione per controllare se il layerToIgnore interagisce
    {
        return InteractButtonIsPressed > 0;
    }
    
    public static void SwitchToUIInput()
    {
        ActionMap.Player.Disable();
        ActionMap.UIControl.Enable();
    }

    public static void SwitchToPlayerInput()
    {
        ActionMap.Player.Enable();
        ActionMap.UIControl.Disable();
    }



    public static void Initialize()
    {
        ActionMap = new ActionMap();
        ActionMap.Enable();
    }
}
