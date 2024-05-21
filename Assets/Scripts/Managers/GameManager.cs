using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<float> OnTemporaryOffInputs;
    public static Action OnKeyObtained;
    public static Action OnKeyUsed;
    //Var
    public static bool playerIsOnTargertPoint;
    public static bool isHoldingAnObject;
    public static bool playerIsOnGrid;
    public static bool isColliding;
    public static bool playerHasKey;

    private void Awake()
    {
        isHoldingAnObject = false;
        playerIsOnTargertPoint = true;
    }
        
        

    private void OnEnable()
    {
        OnTemporaryOffInputs += ReEenableInputsOnDelay;
        OnKeyObtained += KeyObtained;
        OnKeyUsed += KeyUsed;
        //RoomTrigger.OnChangingRoom += ReEenableInputsOnDelay;
    }

    private void OnDisable()
    {
        OnTemporaryOffInputs -= ReEenableInputsOnDelay;
        //RoomTrigger.OnChangingRoom -= ReEenableInputsOnDelay;
    }




    public static void TimeScale(float scale)
    {
        Time.timeScale = scale;
    }


    void ReEenableInputsOnDelay(float time)
    {
        StartCoroutine(OnOffInputDelay(time));
    }

    IEnumerator OnOffInputDelay(float time)
    {
        InputManager.ActionMap.Player.Disable();
        yield return new WaitForSeconds(time);
        InputManager.ActionMap.Player.Enable();
    }

    void KeyObtained()
    {
        playerHasKey = true;
    }

    void KeyUsed()
    {
        playerHasKey = false;
    }
}
