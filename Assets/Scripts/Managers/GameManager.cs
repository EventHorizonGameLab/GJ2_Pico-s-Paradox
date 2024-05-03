using System;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    //Var
    public static bool PlayerIsOnGrid;
    public static bool IsHoldingAnObject;

    private void Awake()
    {
        IsHoldingAnObject = false;
        PlayerIsOnGrid = true;
        
    }

    
  

    public static void TimeScale(float scale)
    {
        Time.timeScale = scale;
    }
}
