using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ResourceClass
{
    public ResourceScriptable scriptable;
    public float amount;
    public float previousAmount;
    public float gainedLastTimeframe;
}
public class ResourceBank : MonoBehaviour
{
    public List<ResourceClass> Resources;

    [SerializeField] private float timeBetweenGainChecks = 1f;
    [SerializeField] private float gainCheckTimer;

    public UnityEvent<ResourceClass> onSpendMoneyResource;
    
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
        for (int i = 0; i < Resources.Count; i++)
        {
            Resources[i].gainedLastTimeframe = Resources[i].amount - Resources[i].previousAmount;
            Resources[i].previousAmount = Resources[i].amount;
        }
    }

    public void AddResource(ResourceScriptable resourceScriptable, float amount)
    {
        ResourceClass resource = ResourceFromScriptable(resourceScriptable);
        if (resource != null)
        {
            resource.amount += amount;
        }
        else
        {
            ResourceClass newResource = new ResourceClass();
            newResource.scriptable = resourceScriptable;
            newResource.amount = amount;
            Resources.Add(newResource);
        }
    }

    public void TrySpendResource(ResourceScriptable resourceScriptable, float amount)
    {
        if (CanAfford(resourceScriptable, amount))
        {
            SpendResource(resourceScriptable, amount);
        }
    }

    private void SpendResource(ResourceScriptable resourceScriptable, float amount)
    {
        AddResource(resourceScriptable, -amount);
        onSpendMoneyResource.Invoke(ResourceFromScriptable(resourceScriptable));
    }

    public bool CanAfford(ResourceScriptable resourceScriptable, float amount)
    {
        ResourceClass resource = ResourceFromScriptable(resourceScriptable);
        if (resource == null || resource.amount < amount)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    


    private ResourceClass ResourceFromScriptable(ResourceScriptable resourceScriptable)
    {
        for (int i = 0; i < Resources.Count; i++)
        {
            if (Resources[i].scriptable == resourceScriptable)
            {
                return Resources[i];
            }
        }

        return null;
    }
}
