using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI actualHour;
    public TextMeshProUGUI actualYear;
    public TextMeshProUGUI answerCodeTxt;

    [Header("Audio Control")]
    public Slider masterAudioSlider;
    public TextMeshProUGUI masterVolumeTxt;
    [Space]
    public Slider musicSlider;
    public TextMeshProUGUI musicVolumeTxt;
    [Space]
    public Slider sfxSlider;
    public TextMeshProUGUI sfxVolumeTxt;
    [Space]
    public Slider dialogueSlider;
    public TextMeshProUGUI dialoguesVolumeTxt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
