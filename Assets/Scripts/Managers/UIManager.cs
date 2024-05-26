using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;

    private void OnEnable()
    {
        Gustavo.lose += Lose;
        WinTrigger.win += Win;
    }

    private void OnDisable()
    {
        Gustavo.lose -= Lose;
        WinTrigger.win -= Win;
    }
    private void Lose()
    {
        BGM.OnEndGame?.Invoke();
        losePanel.SetActive(true);
        GameManager.TimeScale(0);
    }

    private void Win()
    {
        BGM.OnEndGame?.Invoke();
        winPanel.SetActive(true);
        GameManager.TimeScale(0);
    }
    
}
