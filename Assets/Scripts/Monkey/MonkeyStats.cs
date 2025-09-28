using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyStats : MonoBehaviour
{


    public int level = 1;
    public float prodPerSecond = 4;
    public float prodGrowth = 1.5f;
    public float stressPerSecond = 7.5f;
    public float idleStressLower = 10f;
    public float currentStress = 0;
    [SerializeField] float maxStress = 100;

    [SerializeField] bool idle = false;
    [SerializeField] bool testing = false;

    private MonkeyController monkeyController;

    private void Start()
    {
        
        monkeyController = GetComponent<MonkeyController>();

    }

    public void UpgradeMonkey()
    {

        level++;

        prodPerSecond *= prodGrowth;

    }

    public void StressChange(float stressRelief, bool percentage = false)
    {

        //change stress growth based on objects nearby
        stressPerSecond += percentage ? stressRelief * stressPerSecond : stressRelief;

    }

    public float WorkMonkey(float tickCount)
    {
        //add up production over time
        float generatedProd = prodPerSecond / tickCount;

        //apply debuff to production based on current stress
        float stressPercent = 1 - (currentStress / maxStress);
        if (stressPercent < 0.1f)
            stressPercent = 0.1f;

        //manage Stress While working
        currentStress += stressPerSecond / tickCount;
        if (currentStress < 0)
            currentStress = 0;
        else if (currentStress > maxStress)
            currentStress = maxStress;

        monkeyController.CheckWorkMood();

        return generatedProd * stressPercent;

    }

    public void IdleMonkey(float tickCount)
    {

        //lower stress while idle

        monkeyController.ChangeMood(MonkeyMode.sleeping);

        if(currentStress > 0)
            currentStress -= idleStressLower / tickCount;
        else
            currentStress = 0;

    }
    /*
    float testTimer;
    float testProduction;

    private void Update()
    {

        if (!testing)
            return;

        testTimer += Time.deltaTime;

        if (testTimer > 0.25)
        {

            testTimer = 0;

            if (idle)
                IdleMonkey(4);
            else
            {
                print (WorkMonkey(4));
            }

        }

    }
    */

}
