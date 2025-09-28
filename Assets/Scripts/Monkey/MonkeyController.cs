using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyController : MonoBehaviour
{

    private MonkeyStats monkeyStats;
    public MonkeyMode myMode = MonkeyMode.idle;
    private Animator myAnimator;

    void Start()
    {
        
        monkeyStats = GetComponent<MonkeyStats>();
        myAnimator = GetComponentInChildren<Animator>();

    }

    public void CheckWorkMood()
    {

        if(monkeyStats.currentStress > 70)
        {

            ChangeMood(MonkeyMode.angry);

        }
        else
        {

            ChangeMood(MonkeyMode.typing);

        }

    }

    public void ChangeMood(MonkeyMode mood)
    {

        myMode = mood;

        myAnimator.SetInteger("MoodIndicator", (int)myMode);

    }

    public void OpenJobSelection()
    {

        JobSelector.instance.OpenJobSelector(monkeyStats);

    }
}

public enum MonkeyMode
{
    idle, walking,
    typing, sleeping,
    angry, Yippee
}
