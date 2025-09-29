using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductivityManager : MonoBehaviour
{

    public static ProductivityManager instance;

    public float ticksPerSecond = 4;
    public List<Task> currentTasks = new();
    public List<MonkeyStats> idleMonkeys = new();
    private float timer;
    public bool isTesting = false;

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {

        timer += Time.deltaTime;

        //running checks timed based on the ticks Per second
        if (timer >= 1 / ticksPerSecond)
        {

            timer = 0;
            
            // Give Idle Monkey's a break
            foreach (MonkeyStats monkey in idleMonkeys)
                monkey.IdleMonkey(ticksPerSecond);
            
            // run productivity checks for the tasks
            for (int i = 0; i < currentTasks.Count; i++)
            {

                if (currentTasks[i].WorkTask(ticksPerSecond))
                {

                    TaskManager.instance.FinishTask(currentTasks[i]);

                    //remove monkey's from task
                    foreach(MonkeyStats workingMonkey in currentTasks[i].workingMonkeys)
                    {
                        workingMonkey.GetComponent<MonkeyController>().ChangeMood(MonkeyMode.Yippee);

                        idleMonkeys.Add(workingMonkey);
                        //trigger monkey yippee and idle

                    }


                    //get paid
                    ResourceBank.instance.AddResource(ResourceBank.instance.Resources[0].scriptable, currentTasks[i].moneyReward);
                    ResourceBank.instance.AddResource(ResourceBank.instance.Resources[1].scriptable, currentTasks[i].cinnaPoints);

                    currentTasks.RemoveAt(i);
                    i--;

                }

            }

        }

    }

    public void SetMonkeyToTask(MonkeyStats monkey, int taskIndex)
    {

        if(idleMonkeys.Contains(monkey))
            idleMonkeys.Remove(monkey);
        else
            foreach(Task currentTask in currentTasks)
                if (currentTask.workingMonkeys.Contains(monkey))
                {
                    currentTask.workingMonkeys.Remove(monkey);
                    break;
                }

        if (taskIndex >= 0)
            currentTasks[taskIndex].workingMonkeys.Add(monkey);
        else
            idleMonkeys.Add(monkey);
        
    }

}
