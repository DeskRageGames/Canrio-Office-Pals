using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskInfo", menuName = "TaskInfo")]
public class TaskInfoHolder : ScriptableObject
{
    //used to generate task names rather than having a list of premade options
    public string[] characterNames;
    public string[] taskNames;

    public string CreateTaskName()
    {

        //Name example "Design outfit for Kuromi"

        string taskName = string.Format(taskNames[Random.Range(0, taskNames.Length)], characterNames[Random.Range(0, characterNames.Length)]);

        return taskName;

    }

}
