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

    private void FixedUpdate()
    {

        myAnimator.SetInteger("MoodIndicator", (int)myMode);

    }

    public void ChangeMood(MonkeyMode mood)
    {

        myMode = mood;

    }

}

public enum MonkeyMode
{
    idle, walking,
    typing, sleeping,
    angry, Yippee
}
