using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwap : MonoBehaviour
{
    #region Scripts
    [SerializeField] private TimeManager timeManager;
    #endregion

    [Tooltip("The year in game that is going to change")]
    public float yearToChange;
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
        yearToChange = timeManager.actualYear;

        for (int i = 0; i < yearsAlongTime.Length; i++)
        {
            if (yearsAlongTime[i] == yearToChange)
            {
                for (int j = 0; j < objectsAlongTime.Length; j++)
                {
                    if(j != i)
                    {
                        objectsAlongTime[i].SetActive(true);   
                    }
                    else
                    {
                        objectsAlongTime[i].SetActive(false);
                    }
                }
            }
        }
    }
}
