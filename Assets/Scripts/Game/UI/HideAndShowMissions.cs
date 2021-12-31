using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndShowMissions : MonoBehaviour
{
    #region Variables
    [Header ("Reference")]
    [SerializeField] private GameObject missionLog;
    [HideInInspector]
    public bool hasReceivedMissions;
    #endregion

    private void Awake()
    {
        missionLog.SetActive(false);
    }

    private void Update()
    {
        if (hasReceivedMissions)
            missionLog.SetActive(true);
    }
}
