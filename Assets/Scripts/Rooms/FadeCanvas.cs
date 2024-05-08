using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeCanvas : MonoBehaviour
{
    // Events
    public static Action<float> OnChangingRoom;

    //Var
    [SerializeField] private Image blackPanel;
    

    private void Awake()
    {
        SetAlpha(0f);
    }
        

    private void OnEnable()
    {
        OnChangingRoom += HandleRoomChange;
    }

    private void OnDisable()
    {
        OnChangingRoom -= HandleRoomChange;
    }

    private void HandleRoomChange(float totalTransitionTime)
    {
        StartCoroutine(FadeToBlackAndBack(totalTransitionTime));
    }

    private IEnumerator FadeToBlackAndBack(float totalTransitionTime)
    {
        float fadeTime = totalTransitionTime / 4; // One part for fade in and one for fade out
        float holdTime = totalTransitionTime / 2; // Two parts for holding the black screen

        // Fade to black
        yield return StartCoroutine(FadeAlpha(0f, 1f, fadeTime));

        // Hold the black screen for the hold time
        yield return new WaitForSeconds(holdTime);

        // Fade to transparent
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
        SetAlpha(endAlpha); // Ensure the final alpha value is set
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
