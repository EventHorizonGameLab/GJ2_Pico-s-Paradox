using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuComponent : MonoBehaviour
{
    public static MenuComponent instance;

    public GameObject settingsPanel;
    public GameObject showControlsPanel;
    public GameObject creditsPanel;
    public GameObject UIPanel;
    public GameObject menuPanel;
    public GameObject backPanel;
    public int playSceneNumber;
    public GameObject resumeButton;
    [SerializeField] private TMPro.TMP_Dropdown resolutionDropDown;

    private GameObject lastPanel;

    [Header("Buttons Refs")]
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject creditsFirstButton;
    [SerializeField] GameObject controlFirstButton;
    [SerializeField] GameObject settingFirstButton;



    //[HideInInspector] public bool isFullScreen = false;



    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;


    private int currentResolutionIndex = 0;
    [HideInInspector] public Resolution resolution;

    [Header("pause panel")]
    [SerializeField] GameObject pausePanel;
    bool isPause;

    [Header("audio")]
    [SerializeField] AudioData audioData;

    private void Awake()  // non avevi messo il singleton
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
        EventSystem.current.SetSelectedGameObject(playButton);
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
        lastPanel = null;
        InputManager.SwitchToPlayerInput();
        menuPanel.SetActive(false);
        backPanel.SetActive(false);
        SceneManager.LoadScene(playSceneNumber);
    }


    public void OnSettingsButton()
    {
        if (lastPanel == null)
        {
            lastPanel = settingsPanel;
        }

        EventSystem.current.SetSelectedGameObject(settingFirstButton);
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);

    }

    public void OnShowControlsButton()
    {
        EventSystem.current.SetSelectedGameObject(controlFirstButton);
        if (lastPanel == null)
        {
            lastPanel = showControlsPanel;
        }
        UIPanel.SetActive(true);
        showControlsPanel.SetActive(true);
        menuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void OnCredits()
    {
        EventSystem.current.SetSelectedGameObject(creditsFirstButton);
        if (lastPanel == null)
        {
            lastPanel = creditsPanel;
        }
        menuPanel.SetActive(false);
        creditsPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    public void OnGoBackToMenu()
    {
        EventSystem.current.SetSelectedGameObject(playButton);
        ReturnToLastPanel();
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
            InputManager.SwitchToUIInput();
        }

        else
        {
            GameManager.TimeScale(1);
            InputManager.SwitchToPlayerInput();
            lastPanel = null;
            settingsPanel.SetActive(false);
            showControlsPanel.SetActive(false);
            creditsPanel.SetActive(false);
            menuPanel.SetActive(false);
            pausePanel.SetActive(false);
        }

        pausePanel.SetActive(isPause);
    }

    public void OnReturnToMain()
    {
        SceneManager.LoadScene(0);
        lastPanel = menuPanel;
        ReturnToLastPanel();
        backPanel.SetActive(true);
    }

    public void OnCLoseShowControl()
    {
        EventSystem.current.SetSelectedGameObject(playButton);

        showControlsPanel.SetActive(false);
        ReturnToLastPanel();
    }

    public void PlayButtonSound()
    {
        AudioManager.instance.PlaySFX(audioData.sfx_menuButton);
    }

    public void ReturnToLastPanel()
    {
        settingsPanel.SetActive(false);
        showControlsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        menuPanel.SetActive(false);
        pausePanel.SetActive(false);
        lastPanel.SetActive(true);
    }
}

