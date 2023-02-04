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

    [Header("Add times to the time")]
    public float addTime = 0;
    public float addToTime = 1;
    public bool addOrLessTime = false;

    [Header("Boundaries")]
    public float maxYear = 2013;
    public float minYear = 1965;

    [SerializeField] private VideoPlayer staticVideo;
    [SerializeField] private GameObject staticObjectVideo;

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
		DialogueManager.GetInstance().InvokeStoryFunction("setYear", actualYear);

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
