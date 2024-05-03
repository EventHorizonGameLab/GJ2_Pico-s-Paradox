using System;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    //Var
    public static bool playerIsOnTargertPoint;
    public static bool isHoldingAnObject;
    public static bool playerIsOnGrid;

    private void Awake()
    {
        isHoldingAnObject = false;
        playerIsOnTargertPoint = true;
        
    }

    
  

    public static void TimeScale(float scale)
    {
        Time.timeScale = scale;
    }
}
