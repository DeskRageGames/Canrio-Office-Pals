using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{ 
    [SerializeField] private Transform resourceDisplayParent;
    [SerializeField] private GameObject displaySlotPrefab;
    
    [SerializeField] private float timeBetweenDisplayChecks = 0.5f;
    [SerializeField] private float displayCheckTimer;

    private ResourceDisplaySlot[] displaySlots = Array.Empty<ResourceDisplaySlot>();
    private ResourceBank bank;

    private void Start()
    {
        bank = ResourceBank.instance;
    }

    private void Update()
    {
        displayCheckTimer -= Time.deltaTime;
        if (displayCheckTimer <= 0)
        {
            displayCheckTimer = timeBetweenDisplayChecks;
            DisplayResources();
        }
    }

    private void DisplayResources()
    {
        if (bank.Resources.Length != displaySlots.Length)
        {
            DestroyResourceDisplay();
            CreateResourceDisplay();
        }
        
        for (int i = 0; i < displaySlots.Length; i++)
        {
            displaySlots[i].amountText.text = Mathf.FloorToInt(bank.Resources[i].amount).ToString();
            displaySlots[i].gainText.text = Mathf.FloorToInt(bank.Resources[i].gainedLastTimeframe).ToString();
        }
    }

    private void DestroyResourceDisplay()
    {
        if(displaySlots == null || displaySlots.Length == 0) return;
        for (int i = 0; i < displaySlots.Length; i++)
        {
            Destroy(displaySlots[i].gameObject);
        }
    }

    private void CreateResourceDisplay()
    {
        displaySlots = new ResourceDisplaySlot[bank.Resources.Length];

        for (int i = 0; i < bank.Resources.Length; i++)
        {
            displaySlots[i] = Instantiate(displaySlotPrefab, resourceDisplayParent).GetComponent<ResourceDisplaySlot>();
            displaySlots[i].iconImage.sprite = bank.Resources[i].scriptable.icon;
        }
        
        resourceDisplayParent.GetComponent<HorizontalLayoutGroup>().SetLayoutHorizontal();
    }
}
