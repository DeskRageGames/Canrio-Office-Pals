using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ResourceStruct
{
    public ResourceScriptable scriptable;
    public float amount;
    public float previousAmount;
    public float gainedLastTimeframe;
}
public class ResourceBank : MonoBehaviour
{
    public ResourceStruct[] Resources;

    [SerializeField] private float timeBetweenGainChecks = 1f;
    [SerializeField] private float gainCheckTimer;

    public static ResourceBank instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void FixedUpdate()
    {
       RunGainCheckTimer();
    }

    private void RunGainCheckTimer()
    {
        if (gainCheckTimer >= 0)
        {
            gainCheckTimer -= Time.fixedDeltaTime;

            if (gainCheckTimer <= 0)
            {
                gainCheckTimer = timeBetweenGainChecks;
                GainCheck();
            }
        }
    }

    private void GainCheck() // checks how many resources were gained in the last timeframe
    {
        for (int i = 0; i < Resources.Length; i++)
        {
            Resources[i].gainedLastTimeframe = Resources[i].amount - Resources[i].previousAmount;
            Resources[i].previousAmount = Resources[i].amount;
        }
    }
}
