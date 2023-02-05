using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private PlayerMove m_PlayerInput;
    private TimeManager timeManager = null;
    private UiManager uiManager = null;
    private CameraPanNPinch CameraPanNPinch = null;
    private DialogueManager dialogueManager = null;

    [SerializeField] private GameObject pressAnyKey;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject panelCode;
    [SerializeField] private GameObject audioSettings;
    [SerializeField] private GameObject yearInfo;
    [SerializeField] private GameObject yearInfo1;
    [SerializeField] private GameObject controllers;

    [SerializeField] private bool isGameStart = true;

    // Start is called before the first frame update
    void Awake()
    {
        m_PlayerInput = new PlayerMove();
        timeManager = FindObjectOfType<TimeManager>();
        uiManager = FindObjectOfType<UiManager>();
        CameraPanNPinch = FindObjectOfType<CameraPanNPinch>();
        dialogueManager = FindObjectOfType<DialogueManager>();

        PressAnyKey(true);
    }

    private void PressAnyKey(bool active)
    {
        timeManager.SetStatic(active);

        isGameStart = active;
        pressAnyKey.SetActive(active);
        mainMenu.SetActive(!active);
        panelCode.SetActive(!active);
        audioSettings.SetActive(!active);
        yearInfo.SetActive(!active);
        yearInfo1.SetActive(!active);
        controllers.SetActive(!active);
        CameraPanNPinch.canMove = false;
        timeManager.addOrLessTime = !active;
    }

    public void StartGame(bool active)
    {
        mainMenu.SetActive(active);
        pressAnyKey.SetActive(!active);
        isGameStart = !active;
    }

    public void PlayGame(bool active)
    {
        mainMenu.SetActive(!active);
        panelCode.SetActive(!active);
        audioSettings.SetActive(!active);
        yearInfo.SetActive(active);
        controllers.SetActive(active);
    }

    public void AudioSettings(bool active)
    {
        mainMenu.SetActive(!active);
        audioSettings.SetActive(active);
        CameraPanNPinch.canMove = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isGameStart)
        {
            if (m_PlayerInput.CameraMove.PressAnyKey.IsPressed())
                StartGame(true);
        }
    }

    #region Input Enable / Disable
    private void OnEnable()
    {
        m_PlayerInput.Enable();
    }
    private void OnDisable()
    {
        m_PlayerInput.Disable();
    }
    #endregion
}