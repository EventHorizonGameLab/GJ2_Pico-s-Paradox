using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<float> OnTemporaryOffInputs;
    public static Action OnKeyObtained;
    public static Action OnKeyUsed;
    public static Action<bool> OnObjectInCorrectPos;
    
    //Var
    public static bool playerIsOnTargertPoint;
    public static bool isHoldingAnObject;
    public static bool playerIsOnGrid;
    public static bool isColliding;
    public static bool playerHasKey;
    int objectsInCorrectPos;

    private void Awake()
    {
        isHoldingAnObject = false;
        playerIsOnTargertPoint = true;
        playerHasKey = false;
        objectsInCorrectPos = 0;
    }
        
        

    private void OnEnable()
    {
        OnTemporaryOffInputs += ReEenableInputsOnDelay;
        OnKeyObtained += KeyObtained;
        OnKeyUsed += KeyUsed;
        OnObjectInCorrectPos += MovingObjectsChecker;
        //RoomTrigger.OnChangingRoom += ReEenableInputsOnDelay;
    }

    private void OnDisable()
    {
        OnTemporaryOffInputs -= ReEenableInputsOnDelay;
        OnKeyObtained -= KeyObtained;
        OnKeyUsed -= KeyUsed;
        OnObjectInCorrectPos -= MovingObjectsChecker;
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

    public void MovingObjectsChecker(bool value)
    {

        if(value) { objectsInCorrectPos++; } else { objectsInCorrectPos--; }
        if(objectsInCorrectPos == 4) OnKeyObtained();
        //debug
        if (objectsInCorrectPos == 4) Debug.Log("Hai posizionato" + objectsInCorrectPos + "oggetti ed è" + playerHasKey +"che hai la chiave");
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
