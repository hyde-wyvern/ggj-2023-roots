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

    private void Awake()
    {
        timeManager = FindObjectOfType<TimeManager>();
    }
}
