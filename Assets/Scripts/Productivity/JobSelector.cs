using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JobSelector : MonoBehaviour
{

    public static JobSelector instance;

    [SerializeField] ButtonInfo[] taskButtons;

    private MonkeyStats selectedMonkey;

    bool selectionOpen = false;

    private void Awake()
    {
        instance = this;
    }

    public void OpenJobSelector(MonkeyStats jobSeeker)
    {

        selectionOpen = true;

        selectedMonkey = jobSeeker;

        List<Task> tasks = new List<Task>(ProductivityManager.instance.currentTasks);

        for(int i = 0; i < tasks.Count; i++)
        {
            ButtonInfo recInfo = taskButtons[i];
            Task setInfo = tasks[i];

            recInfo.mainObject.SetActive(true);

            recInfo.name.text = setInfo.taskName;
            recInfo.monkeys.text = setInfo.workingMonkeys.Count.ToString();

            if (setInfo.moneyReward > 0)
            {
                recInfo.money.transform.parent.gameObject.SetActive(true);
                recInfo.money.text = setInfo.moneyReward.ToString();
            }
            else
                recInfo.money.transform.parent.gameObject.SetActive(false);
            if (setInfo.cinnaPoints > 0)
            {
                recInfo.cinna.transform.parent.gameObject.SetActive(true);
                recInfo.cinna.text = setInfo.cinnaPoints.ToString();
            }
            else
                recInfo.cinna.transform.parent.gameObject.SetActive(false);

        }

        transform.GetChild(0).gameObject.SetActive(true);

    }

    public void JobChoice(int taskIndex)
    {

        ProductivityManager.instance.SetMonkeyToTask(selectedMonkey, taskIndex);

        Close();

    }

    public void Close()
    {

        transform.GetChild(0).gameObject.SetActive(false);

        foreach (ButtonInfo button in taskButtons)
            button.mainObject.SetActive(false);

        selectionOpen = false;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray direction = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(direction, out hit))
            {

                if (hit.collider.gameObject.TryGetComponent<MonkeyController>(out MonkeyController controller))
                    controller.OpenJobSelection();

            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}

[System.Serializable]
public class ButtonInfo
{
    public GameObject mainObject;
    public TMP_Text name, money,
        cinna, monkeys;


}
