using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressReducer : MonoBehaviour
{

    public float stressReduction = 0.8f;

    public static List<StressReducer> stressReducers = new();

    private void Awake()
    {

        if(stressReducers == null)
        {

            stressReducers = new();
        }

        stressReducers.Add(this);

    }

}
