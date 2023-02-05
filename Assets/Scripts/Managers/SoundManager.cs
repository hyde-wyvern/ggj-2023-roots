using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private UiManager uiManager;
    [SerializeField] private GameManager gameManager;

    public AudioClip[] menus;
    public AudioClip[] levels;

    public AudioMixer audioMixer = null;

    [HideInInspector] public AudioSource music = null;
    public AudioSource buttonSFX = null;

    private int musicIdx = 0;
    public bool isPlayingSong { get; private set; }

    void Awake()
    {
        uiManager = FindObjectOfType<UiManager>();
        music = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        #region Master Volume
        audioMixer.SetFloat("Master", uiManager.masterAudioSlider.value);

        if (uiManager.masterAudioSlider.value == -20)
            audioMixer.SetFloat("Master", -80f);

        switch (uiManager.masterAudioSlider.value)
        {
            case -20:
                uiManager.masterVolumeTxt.text = "00";
                break;
            case -19:
                uiManager.masterVolumeTxt.text = "05";
                break;
            case -18:
                uiManager.masterVolumeTxt.text = "10";
                break;
            case -17:
                uiManager.masterVolumeTxt.text = "15";
                break;
            case -16:
                uiManager.masterVolumeTxt.text = "20";
                break;
            case -15:
                uiManager.masterVolumeTxt.text = "25";
                break;
            case -14:
                uiManager.masterVolumeTxt.text = "30";
                break;
            case -13:
                uiManager.masterVolumeTxt.text = "35";
                break;
            case -12:
                uiManager.masterVolumeTxt.text = "40";
                break;
            case -11:
                uiManager.masterVolumeTxt.text = "45";
                break;
            case -10:
                uiManager.masterVolumeTxt.text = "50";
                break;
            case -9:
                uiManager.masterVolumeTxt.text = "55";
                break;
            case -8:
                uiManager.masterVolumeTxt.text = "60";
                break;
            case -7:
                uiManager.masterVolumeTxt.text = "65";
                break;
            case -6:
                uiManager.masterVolumeTxt.text = "70";
                break;
            case -5:
                uiManager.masterVolumeTxt.text = "75";
                break;
            case -4:
                uiManager.masterVolumeTxt.text = "80";
                break;
            case -3:
                uiManager.masterVolumeTxt.text = "85";
                break;
            case -2:
                uiManager.masterVolumeTxt.text = "90";
                break;
            case -1:
                uiManager.masterVolumeTxt.text = "95";
                break;
            case 0:
                uiManager.masterVolumeTxt.text = "100";
                break;
            default:
                break;
        }
        #endregion

        #region Music Volume
        audioMixer.SetFloat("Music", uiManager.musicSlider.value);

        if (uiManager.musicSlider.value == -20)
            audioMixer.SetFloat("Music", -80f);

        switch (uiManager.musicSlider.value)
        {
            case -20:
                uiManager.musicVolumeTxt.text = "00";
                break;
            case -19:
                uiManager.musicVolumeTxt.text = "05";
                break;
            case -18:
                uiManager.musicVolumeTxt.text = "10";
                break;
            case -17:
                uiManager.musicVolumeTxt.text = "15";
                break;
            case -16:
                uiManager.musicVolumeTxt.text = "20";
                break;
            case -15:
                uiManager.musicVolumeTxt.text = "25";
                break;
            case -14:
                uiManager.musicVolumeTxt.text = "30";
                break;
            case -13:
                uiManager.musicVolumeTxt.text = "35";
                break;
            case -12:
                uiManager.musicVolumeTxt.text = "40";
                break;
            case -11:
                uiManager.musicVolumeTxt.text = "45";
                break;
            case -10:
                uiManager.musicVolumeTxt.text = "50";
                break;
            case -9:
                uiManager.musicVolumeTxt.text = "55";
                break;
            case -8:
                uiManager.musicVolumeTxt.text = "60";
                break;
            case -7:
                uiManager.musicVolumeTxt.text = "65";
                break;
            case -6:
                uiManager.musicVolumeTxt.text = "70";
                break;
            case -5:
                uiManager.musicVolumeTxt.text = "75";
                break;
            case -4:
                uiManager.musicVolumeTxt.text = "80";
                break;
            case -3:
                uiManager.musicVolumeTxt.text = "85";
                break;
            case -2:
                uiManager.musicVolumeTxt.text = "90";
                break;
            case -1:
                uiManager.musicVolumeTxt.text = "95";
                break;
            case 0:
                uiManager.musicVolumeTxt.text = "100";
                break;
            default:
                break;
        }
        #endregion

        #region SFX Volume
        audioMixer.SetFloat("SoundFX", uiManager.sfxSlider.value);

        if (uiManager.sfxSlider.value == -20)
            audioMixer.SetFloat("SoundFX", -80f);

        switch (uiManager.sfxSlider.value)
        {
            case -20:
                uiManager.sfxVolumeTxt.text = "00";
                break;
            case -19:
                uiManager.sfxVolumeTxt.text = "05";
                break;
            case -18:
                uiManager.sfxVolumeTxt.text = "10";
                break;
            case -17:
                uiManager.sfxVolumeTxt.text = "15";
                break;
            case -16:
                uiManager.sfxVolumeTxt.text = "20";
                break;
            case -15:
                uiManager.sfxVolumeTxt.text = "25";
                break;
            case -14:
                uiManager.sfxVolumeTxt.text = "30";
                break;
            case -13:
                uiManager.sfxVolumeTxt.text = "35";
                break;
            case -12:
                uiManager.sfxVolumeTxt.text = "40";
                break;
            case -11:
                uiManager.sfxVolumeTxt.text = "45";
                break;
            case -10:
                uiManager.sfxVolumeTxt.text = "50";
                break;
            case -9:
                uiManager.sfxVolumeTxt.text = "55";
                break;
            case -8:
                uiManager.sfxVolumeTxt.text = "60";
                break;
            case -7:
                uiManager.sfxVolumeTxt.text = "65";
                break;
            case -6:
                uiManager.sfxVolumeTxt.text = "70";
                break;
            case -5:
                uiManager.sfxVolumeTxt.text = "75";
                break;
            case -4:
                uiManager.sfxVolumeTxt.text = "80";
                break;
            case -3:
                uiManager.sfxVolumeTxt.text = "85";
                break;
            case -2:
                uiManager.sfxVolumeTxt.text = "90";
                break;
            case -1:
                uiManager.sfxVolumeTxt.text = "95";
                break;
            case 0:
                uiManager.sfxVolumeTxt.text = "100";
                break;
            default:
                break;
        }
        #endregion

        #region Dialogues Volume
        audioMixer.SetFloat("Dialogues", uiManager.dialogueSlider.value);

        if (uiManager.dialogueSlider.value == -20)
            audioMixer.SetFloat("Dialogues", -80f);

        switch (uiManager.dialogueSlider.value)
        {
            case -20:
                uiManager.dialoguesVolumeTxt.text = "00";
                break;
            case -19:
                uiManager.dialoguesVolumeTxt.text = "05";
                break;
            case -18:
                uiManager.dialoguesVolumeTxt.text = "10";
                break;
            case -17:
                uiManager.dialoguesVolumeTxt.text = "15";
                break;
            case -16:
                uiManager.dialoguesVolumeTxt.text = "20";
                break;
            case -15:
                uiManager.dialoguesVolumeTxt.text = "25";
                break;
            case -14:
                uiManager.dialoguesVolumeTxt.text = "30";
                break;
            case -13:
                uiManager.dialoguesVolumeTxt.text = "35";
                break;
            case -12:
                uiManager.dialoguesVolumeTxt.text = "40";
                break;
            case -11:
                uiManager.dialoguesVolumeTxt.text = "45";
                break;
            case -10:
                uiManager.dialoguesVolumeTxt.text = "50";
                break;
            case -9:
                uiManager.dialoguesVolumeTxt.text = "55";
                break;
            case -8:
                uiManager.dialoguesVolumeTxt.text = "60";
                break;
            case -7:
                uiManager.dialoguesVolumeTxt.text = "65";
                break;
            case -6:
                uiManager.dialoguesVolumeTxt.text = "70";
                break;
            case -5:
                uiManager.dialoguesVolumeTxt.text = "75";
                break;
            case -4:
                uiManager.dialoguesVolumeTxt.text = "80";
                break;
            case -3:
                uiManager.dialoguesVolumeTxt.text = "85";
                break;
            case -2:
                uiManager.dialoguesVolumeTxt.text = "90";
                break;
            case -1:
                uiManager.dialoguesVolumeTxt.text = "95";
                break;
            case 0:
                uiManager.dialoguesVolumeTxt.text = "100";
                break;
            default:
                break;
        }
        #endregion
    }

    public void OnPressButton(AudioClip clip)
    {
        buttonSFX.clip = clip;
        buttonSFX.Play();
    }
}
