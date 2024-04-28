using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager 
{
    public static ActionMap ActionMap;

    static InputManager()
    {
        ActionMap = new ActionMap();
        ActionMap.Enable();
    }

    public static Vector3 Movement => ActionMap.Player.Movement.ReadValue<Vector3>(); // Vettore gi� normalizzato

    public static bool IsMoving (out Vector3 direction) // Funzione utilizzabile in update per muovere il personaggio, restituisce l'input direzionale
    {
        direction = Movement;
        return direction != Vector3.zero;
    }
        
}
