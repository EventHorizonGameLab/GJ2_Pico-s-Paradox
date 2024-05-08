using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeCanvas : MonoBehaviour
{

    //Var
    [SerializeField] private Image blackPanel;
    

    private void Awake()
    {
        SetAlpha(0f);
    }
        

    private void OnEnable()
    {
        RoomTrigger.OnChangingRoom += HandleRoomChange;
    }

    private void OnDisable()
    {
        RoomTrigger.OnChangingRoom -= HandleRoomChange;
    }

    private void HandleRoomChange(float totalTransitionTime)
    {
        StartCoroutine(FadeToBlackAndBack(totalTransitionTime));
    }

    private IEnumerator FadeToBlackAndBack(float totalTransitionTime)
    {
        float fadeTime = totalTransitionTime / 4;
        float holdTime = totalTransitionTime / 2;

        yield return StartCoroutine(FadeAlpha(0f, 1f, fadeTime));
        
        yield return new WaitForSeconds(holdTime);

        yield return StartCoroutine(FadeAlpha(1f, 0f, fadeTime));
    }
        

        

    private IEnumerator FadeAlpha(float startAlpha, float endAlpha, float duration)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, currentTime / duration);
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(endAlpha);
    }

    private void SetAlpha(float alpha)
    {
        if (blackPanel != null)
        {
            Color color = blackPanel.color;
            color.a = alpha;
            blackPanel.color = color;
        }
        else
        {
            Debug.LogError("blackPanel is not assigned!");
        }
    }

}
