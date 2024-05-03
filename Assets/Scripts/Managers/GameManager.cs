using System;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Events
    public static Action<bool> OnPlayerHoldingObject;
    //Var
    public static bool PlayerIsOnGrid;
    public static bool IsHoldingAnObject;

    private void Awake()
    {
        IsHoldingAnObject = false;
        PlayerIsOnGrid = true;
        InputManager.Initialize();
    }

    private void OnEnable()
    {
        OnPlayerHoldingObject += ChangeHoldBool;
    }

    private void OnDisable()
    {
        OnPlayerHoldingObject -= ChangeHoldBool;
    }

    void ChangeHoldBool(bool value)
    {
        IsHoldingAnObject = value;
    }
}
