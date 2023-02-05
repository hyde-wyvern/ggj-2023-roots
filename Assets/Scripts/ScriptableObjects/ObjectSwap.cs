using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwap : MonoBehaviour
{
    #region Scripts
    private TimeManager timeManager;
    #endregion

    [Tooltip("The year in game that is going to change")]
    private float yearToChange;
    [Space]
    [Tooltip("Add in order (past to present)")]
    public GameObject[] objectsAlongTime;

    [Tooltip("Add in order (past to present)")]
    public int[] yearsAlongTime;

    private void Awake()
    {
        timeManager = FindObjectOfType<TimeManager>();
    }

    private void FixedUpdate()
    {
        yearToChange = timeManager.aYear;

        for (int i = 0; i < yearsAlongTime.Length; i++)
        {
            if (yearsAlongTime[i] <= yearToChange)
            {
                Debug.Log("Year " + yearsAlongTime[i]);
                for (int j = 0; j < objectsAlongTime.Length; j++)
                {
                    if(j != i)
                    {
                        objectsAlongTime[j].SetActive(false);   
                    }
                    else
                    {
                        objectsAlongTime[i].SetActive(true);
                    }
                }
            }
        }
    }
}
