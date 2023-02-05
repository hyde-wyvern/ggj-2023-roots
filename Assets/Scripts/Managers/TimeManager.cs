using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;

public class TimeManager : MonoBehaviour
{
	[Header("Yearly Dialogue Settings")]
	public TextAsset timeBaseDialogue;
    public float delay = 0.5f;

    [Header("Actual Year")]
    public float actualYear = 2000;
    public int aYear = 1965;

    [Header("Add times to the time")]
    public float addTime = 0;
    public float addToTime = 1;
    public bool addOrLessTime = false;

    [Header("Boundaries")]
    public float maxYear = 2013;
    public float minYear = 1965;

    [SerializeField] private VideoPlayer staticVideo;
    [SerializeField] private GameObject staticObjectVideo;

    private UiManager uiManager;

    [Header("VHS Effects")]
    [SerializeField] private string[] allMonths = null;
    [SerializeField] private string[] allDays = null;
    [SerializeField] private string[] allYearsCombinations = null;
    [Space]
    [SerializeField] private string[] allTimes = null;
    [SerializeField] private string[] allHourCombinations = null;
    private int randomTimeHour;
    private int randomTimeMinute;
    private string time = null;

    private void Awake()
    {
        uiManager = FindObjectOfType<UiManager>();
        
        SetAllRandomsCombinations();
        aYear = (int)actualYear;
    }

    private void SetAllRandomsCombinations()
    {
        int totalOfYears = (int)maxYear - (int)minYear;
        allYearsCombinations = new string[totalOfYears];
        allHourCombinations = new string[totalOfYears];

        for (int i = 0; i < allYearsCombinations.Length; i++)
        {
            allYearsCombinations[i] = allMonths[Random.Range(0, allMonths.Length)] + " " + allDays[Random.Range(0, allDays.Length)] + " ";

            if(allYearsCombinations[i].Substring(0, 3) == "FEB")
            {
                allYearsCombinations[i] = allMonths[1] + " " + allDays[Random.Range(0, 28)] + " ";
            }
        }

        for (int h = 0; h < allHourCombinations.Length; h++)
        {
            randomTimeHour = Random.Range(0, 12);
            randomTimeMinute = Random.Range(0, 60);

            time = string.Format("{0:00}:{1:00}", randomTimeHour, randomTimeMinute);

            allHourCombinations[h] = allTimes[Random.Range(0, allTimes.Length)] + " " + time;
        }

        SetVHSInfo();
    }

    void Start()
    {
        TravelInTime(0);
    }

    void FixedUpdate()
    {
        if (addOrLessTime)
            SetTime();
    }

    private void SetTime()
    {
        if (addToTime > 0 && actualYear >= maxYear) TravelInTime(0);

        if (addToTime < 0 && actualYear <= minYear) TravelInTime(0);

		AddOrLessTime(addToTime);

		actualYear += addTime * Time.fixedDeltaTime;
        actualYear = Mathf.Clamp(actualYear, minYear, maxYear);

        aYear = (int)actualYear;

        SetVHSInfo();
    }

     private void SetVHSInfo()
    {
        int date = (int)maxYear - aYear;
        //VHS Hour
        uiManager.actualHour.text = allHourCombinations[date - 1];

        //VHS Date
        uiManager.actualYear.text = allYearsCombinations[date - 1] + aYear.ToString("F0");
    }

    private void AddOrLessTime(float timeYear) => addTime += timeYear * Time.fixedDeltaTime;
     
    public void TravelInTime(int travelFactor) {

        if(travelFactor == addToTime) return;

        addToTime = travelFactor;

        switch(addToTime)
        {
            case 0:
                addTime = 0;
				SetStatic(false);
                RunYearlyDialogue();
				break;
            case 1:
                if (addTime < 0) addTime = 0;
				SetStatic(true);
				break;
            case -1:
				if (addTime > 0) addTime = 0;
                SetStatic(true);
				break;
		}
	}
    
    public void RunYearlyDialogue()
    {
		DialogueManager.GetInstance().SetDialogue(timeBaseDialogue);
		DialogueManager.GetInstance().InvokeStoryFunction("setYear", aYear);

		if (addOrLessTime) {
            StopAllCoroutines();
            return; 
        }
        if (DialogueManager.GetInstance().dialogueIsPlaying) {
            StopAllCoroutines();
            return;
        }

        StartCoroutine(YearlyDialogue());
    }

    private IEnumerator YearlyDialogue()
    {
		yield return new WaitForSeconds(delay);
        DialogueManager.GetInstance().StartDialogue();
	}

    
    private void SetStatic(bool showStatic)
    {
		staticObjectVideo.gameObject.SetActive(showStatic);
		addOrLessTime = showStatic;
		if(showStatic) staticVideo.Play();
        else staticVideo.Stop();
	}
}
