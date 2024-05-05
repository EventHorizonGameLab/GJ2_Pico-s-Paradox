using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action TemporaryOffInputs;
    //Var
    public static bool playerIsOnTargertPoint;
    public static bool isHoldingAnObject;
    public static bool playerIsOnGrid;
    public static bool isColliding;

    private void Awake()
    {
        isHoldingAnObject = false;
        playerIsOnTargertPoint = true;
    }
        
        

    private void OnEnable()
    {
        TemporaryOffInputs += JustReleasedObject;
    }

    private void OnDisable()
    {
        TemporaryOffInputs += JustReleasedObject;
    }




    public static void TimeScale(float scale)
    {
        Time.timeScale = scale;
    }


    void JustReleasedObject()
    {
        StartCoroutine(OnOffInputDelay());
    }

    IEnumerator OnOffInputDelay()
    {
        InputManager.ActionMap.Player.Disable();
        yield return new WaitForSeconds(0.2f);
        InputManager.ActionMap.Player.Enable();
    }
}
