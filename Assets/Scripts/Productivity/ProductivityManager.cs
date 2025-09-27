using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductivityManager : MonoBehaviour
{

    public float ticksPerSecond = 4;
    public List<Task> currentTasks = new();
    public List<MonkeyStats> idleMonkeys = new();
    private float timer;
    public bool isTesting = false;
    private void Update()
    {

        if (!isTesting)
            return;

        timer += Time.deltaTime;

        //running checks timed based on the ticks Per second
        if (timer >= 1 / ticksPerSecond)
        {

            print(1 / ticksPerSecond);

            timer = 0;
            
            // Give Idle Monkey's a break
            foreach (MonkeyStats monkey in idleMonkeys)
                monkey.IdleMonkey(ticksPerSecond);
            
            // run productivity checks for the tasks
            for (int i = 0; i < currentTasks.Count; i++)
            {

                if (currentTasks[i].WorkTask(ticksPerSecond))
                {

                    foreach(MonkeyStats workingMonkey in currentTasks[i].workingMonkeys)
                    {
                        idleMonkeys.Add(workingMonkey);
                    }

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
