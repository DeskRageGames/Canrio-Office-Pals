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

    public void UpgradeMonkey()
    {

        level++;

        prodPerSecond *= prodGrowth;

    }

    public void StressChange(float stressRelief, bool percentage = false)
    {

        stressPerSecond += percentage ? stressRelief * stressPerSecond : stressRelief;

    }

    public float WorkMonkey(int tickCount)
    {
        //add up production over time
        float generatedProd = prodPerSecond / tickCount;

        float stressPercent = 1 - (currentStress / maxStress);
        if (stressPercent < 0.1f)
            stressPercent = 0.1f;

        //manage Stress While working
        currentStress += stressPerSecond / tickCount;
        if (currentStress < 0)
            currentStress = 0;
        else if (currentStress > maxStress)
            currentStress = maxStress;


        return generatedProd * stressPercent;

    }

    public void IdleMonkey(int tickCount)
    {

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
