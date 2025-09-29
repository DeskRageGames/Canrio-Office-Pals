using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{

    public static TaskManager instance;
    public Transform[] taskLocations;
    private List<Transform> taskPlacements = new();
    public GameObject taskPrefab;
    public int taskCount = 0;

    public Vector2[] lowHighProd;
    public Vector2[] lowHighMoney;
    public Vector2[] lowHighCinna;

    [SerializeField] TaskInfoHolder taskInfoHolder;

    private void Awake()
    {
        
        instance = this;

    }

    private void Start()
    {

        GenerateTask();
        GenerateTask();
        GenerateTask();

    }

    public void GenerateTask()
    {

        Task generatedTask = new Task();

        //Generate Task name with a job and up to 1 character name
        generatedTask.taskName = taskInfoHolder.CreateTaskName();

        //Get difficulty and set numbers based on the difficulty levels
        int difficultyIndex = Random.Range(0, 2);
        generatedTask.difficulty = (TaskDifficulty)difficultyIndex;

        generatedTask.prodCost = Random.Range((int)lowHighProd[difficultyIndex].x, (int)lowHighProd[difficultyIndex].y);
        generatedTask.moneyReward = Random.Range((int)lowHighMoney[difficultyIndex].x, (int)lowHighMoney[difficultyIndex].y);
        generatedTask.cinnaPoints = Random.Range((int)lowHighCinna[difficultyIndex].x, (int)lowHighCinna[difficultyIndex].y);

        ProductivityManager.instance.currentTasks.Add(generatedTask);

        generatedTask.progressBar = Instantiate(taskPrefab, taskLocations[taskCount]).GetComponent<TaskFiller>();

        taskPlacements.Add(generatedTask.progressBar.transform);

        generatedTask.progressBar.LoadInfo(generatedTask);

        taskCount++;
    }

    public void FinishTask(Task task)
    {

        taskPlacements.Remove(task.progressBar.transform);
        Destroy(task.progressBar.gameObject);

        //gain reward for money and cinna points

        taskCount--;
        
        for(int i = 0; i < taskPlacements.Count; i++)
        {
            
            taskPlacements[i].SetParent(taskLocations[i], false);

        }

        GenerateTask();

    }

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

    public List<MonkeyStats> workingMonkeys = new();

    public bool WorkTask(float ticksPerSecond)
    {
        float productionTotal = 0;

        if(workingMonkeys.Count > 0)
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
