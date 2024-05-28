using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioMixer Mixer;
    [SerializeField] AudioData AudioData;
    bool isPlaying;

    public const string MASTER_KEY = "MasterVolume";
    public const string MUSIC_KEY = "MusicVolume";
    public const string SFX_KEY = "SFXVolume";

    private void Awake()
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

    private void Start()
    {
        LoadVolumes();
    }

    


    void LoadVolumes() // Volumes saved in VolumeSettings.cs
    {
        float masterVolume = PlayerPrefs.GetFloat(MASTER_KEY, 0.5f);
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 0.5f);

        Mixer.SetFloat(VolumeSettings.MIXER_MASTER, Mathf.Log10(masterVolume) * 20); ;
        Mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        Mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }

    public void PlaySFX(AudioClip clip)
    {
        AudioSource audioSource = AudioObjectPooler.SharedInstance.GetPooledObject().GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.gameObject.SetActive(true);
        audioSource.Play();
        StartCoroutine(DeactivationTimer(audioSource.gameObject, audioSource.clip.length));
    }

    public void PlayInLoop(AudioClip clip)
    {
        
        AudioSource audioSource = AudioObjectPooler.SharedInstance.pooledObjects[^1].GetComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.loop = true;

        if (InputManager.Movement != Vector3.zero)
        {
            if (!isPlaying)
            {
                audioSource.gameObject.SetActive(true);
                audioSource.Play();
                isPlaying = true;
            }
        }
        else
        {
            if (isPlaying)
            {
                audioSource.Stop();
                audioSource.gameObject.SetActive(false);
                isPlaying = false;
            }
        }
    }
    IEnumerator DeactivationTimer(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        if (obj != null)
        {
            obj.SetActive(false);
        }
    }

    
    
    





}
