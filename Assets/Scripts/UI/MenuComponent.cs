using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuComponent : MonoBehaviour
{

    public GameObject settingsPanel;
    public GameObject showControlsPanel;
    public GameObject creditsPanel;
    public GameObject UIPanel;

    public int playSceneNumber;

    //[HideInInspector] public bool isFullScreen = false;


    [SerializeField] private TMPro.TMP_Dropdown resolutionDropDown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;


    private int currentResolutionIndex = 0;
    [HideInInspector] public Resolution resolution;

    [SerializeField] GameObject pausePanel;
    bool isPause;
    [SerializeField] GameObject showControlPanel;

    [SerializeField] AudioData audioData;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnDisable()
    {
        try
        {
            InputManager.ActionMap.AlwaysOn.Pause.performed -= ActivatePause;
        }
        finally
        {

        }
    }
    private void Start()
    {
        PlayerPrefs.SetInt("ScreenMode", 0);
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropDown.ClearOptions();


        for (int i = 0; i < resolutions.Length; i++)
        {
            if (!filteredResolutions.Any(x => x.width == resolutions[i].width && x.height == resolutions[i].height))
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + " x " + filteredResolutions[i].height;
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }
    public void OnPlayButton()
    {
        InputManager.ActionMap.AlwaysOn.Pause.performed += ActivatePause;
        SceneManager.LoadScene(playSceneNumber);
        UIPanel.SetActive(false);
        
    }

    public void OnSettingsButton()
    {
        settingsPanel.SetActive(true);

    }

    public void OnShowControlsButton()
    {
        showControlsPanel.SetActive(true);
    }

    public void OnCredits()
    {
        creditsPanel.SetActive(true);
    }
    public void OnGoBackToMenu()
    {
        settingsPanel.SetActive(false);
        showControlsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void OnFullScreen()
    {

        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("changed screen");
        if (Screen.fullScreen)
        {
            PlayerPrefs.SetInt("ScreenMode", 1);
        }

        else
        {
            PlayerPrefs.SetInt("ScreenMode", 0);
        }
    }

    public void SetResolution()
    {
        resolution = filteredResolutions[resolutionDropDown.value];
        if (PlayerPrefs.GetInt("ScreenMode") == 0)
        {
            Screen.SetResolution(resolution.width, resolution.height, true);
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        if (PlayerPrefs.GetInt("ScreenMode") == 1)
        {
            Screen.SetResolution(resolution.width, resolution.height, false);
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        //Screen.SetResolution(resolution.width, resolution.height, true);
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }
    private void ActivatePause(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (pausePanel == null)
        {
            return;
        }
        Debug.Log("tua zia");
        isPause = !isPause;
        Debug.Log(isPause);

        if (isPause)
        {
            GameManager.TimeScale(0);
            InputManager.SwitchToUI();
        }

        else
        {
            GameManager.TimeScale(1);
            InputManager.SwitchToPlayer();
        }

        pausePanel.SetActive(isPause);
    }

    public void OnReturnToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void OnCLoseShowControl()
    {
        showControlPanel.SetActive(false);
    }

    public void PlayButtonSound()
    {
        AudioManager.instance.PlaySFX(audioData.sfx_menuButton);
    }
}

