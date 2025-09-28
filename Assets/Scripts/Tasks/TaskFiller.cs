using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TaskFiller : MonoBehaviour
{

    public TMP_Text taskTitle;
    public TMP_Text progress;
    public Image progressBar;
    private int prodMax = 100;

    public TMP_Text money;
    public TMP_Text cinnaPoints;

    public void LoadInfo(Task task)
    {

        prodMax = (int)task.prodCost;

        taskTitle.text = task.taskName;

        progress.text = "0 / " + prodMax;

        if(task.moneyReward > 0)
        {

            money.transform.parent.gameObject.SetActive(true);

            money.text = task.moneyReward.ToString();

        }

        if(task.cinnaPoints > 0)
        {

            cinnaPoints.transform.parent.gameObject.SetActive(true);

            cinnaPoints.text = task.cinnaPoints.ToString();

        }
    }

    public void ProgressGain(int currentProd)
    {

        progressBar.fillAmount = (float)currentProd / (float)prodMax;

        progress.text = currentProd + " / " + prodMax;

    }

}
