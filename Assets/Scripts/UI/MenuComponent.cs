using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuComponent : MonoBehaviour
{

    public GameObject settingsPanel;
    public GameObject showControlsPanel;

    public int playSceneNumber;

    public void OnPlayButton()
    {
        SceneManager.LoadScene(playSceneNumber);
    }

    public void OnSettingsButton()
    {
        settingsPanel.SetActive(true);

    }

    public void OnShowControlsButton()
    {
        showControlsPanel.SetActive(true);
    }
    public void OnGoBackToMenu()
    {
        settingsPanel.SetActive(false);
        showControlsPanel.SetActive(false);
    }
    public void OnQuitGame()
    {
        Application.Quit();
    }


}
