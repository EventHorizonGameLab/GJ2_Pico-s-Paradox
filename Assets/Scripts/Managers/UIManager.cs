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
        losePanel.SetActive(true);
    }

    private void Win()
    {
        winPanel.SetActive(true);
    }
    
}
