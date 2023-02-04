using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Actual Year")]
    public float actualYear = 2000;

    [Header("Add times to the time")]
    public float addTime = 0;
    public float addToTime = 1;
    public bool addOrLessTime = false;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (addOrLessTime)
            AddOrLessTime(addToTime);

        actualYear += addTime * Time.fixedDeltaTime;
    }

    private void AddOrLessTime(float timeYear)
    {
        addTime += timeYear * Time.fixedDeltaTime;
    }
    public void SetAddTime(float timeYear)
    {
        addToTime = timeYear;
    }

    public void PauseOrPlaySetAddTime(float timeYear)
    {
        addTime = timeYear;
    }

    public void OnMouseDown()
    {
        addOrLessTime = true;
    }

    public void OnMouseUp()
    {
        addOrLessTime = false;
    }
}
