using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{



}


[System.Serializable]
public class Task
{

    public string taskName;
    public TaskFiller progressBar;
    public bool activity;
    public TaskDifficulty difficulty;

    public float prodCurrent;
    public float prodCost;
    
    public int moneyReward;
    public int cinnaPoints;

    public List<MonkeyStats> workingMonkeys;

    public bool WorkTask(float ticksPerSecond)
    {
        float productionTotal = 0;

        foreach (MonkeyStats monkey in workingMonkeys)
            productionTotal += monkey.WorkMonkey(ticksPerSecond);

        return AddProductivity(productionTotal);

    }
    public bool AddProductivity(float production)
    {

        prodCurrent += production;

        progressBar.ProgressGain(Mathf.RoundToInt(prodCurrent));

        if(prodCurrent >= prodCost)
        {

            Debug.Log("Completed Task");

            //complete task gain reward, generate next task

            return true;

        }

        return false;

    }

}

public enum TaskDifficulty
{
    Low, Medium, High
}
