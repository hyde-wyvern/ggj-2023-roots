using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float actualYear = 2000;
    public float addTime = 0;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        actualYear += addTime * Time.fixedDeltaTime;
    }

    public void AddOrLessTime(float timeYear)
    {
        addTime = timeYear;
    }
}
