using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AudioDatabase")]
public class AudioData : ScriptableObject
{
    [SerializeField] public AudioClip music_BGM;
    [SerializeField] public AudioClip music_Gustavo;
    [SerializeField] public AudioClip sfx_ObjectMoving;
    [SerializeField] public AudioClip sfx_looseScreen;
    [SerializeField] public AudioClip sfx_menuButton;
    [SerializeField] public AudioClip sfx_monstearRoar;
    [SerializeField] public AudioClip sfx_openingDoor;
    [SerializeField] public AudioClip sfx_paperInteraction;
    [SerializeField] public AudioClip sfx_puzzleSolved;
    [SerializeField] public AudioClip sfx_puzzleWrong;
    [SerializeField] public AudioClip sfx_interactSound;
    [SerializeField] public AudioClip sfx_victorySound;
}
