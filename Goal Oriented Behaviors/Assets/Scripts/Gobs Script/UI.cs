using UnityEngine;
using TMPro;
using System.Collections;

public class UI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI discontentTimer;

    [SerializeField]
    private TextMeshProUGUI actionTimer;

    [SerializeField]
    private TextMeshProUGUI actionLabel;

    [SerializeField]
    private TextMeshProUGUI goalValues;

    [SerializeField]
    private TextMeshProUGUI discontentLabel;

    public static UI instance;

    private void Awake()
    {
        instance = this;
    }
    public void UpdateTimers(float discontentTime, float actionTime)
    {
        discontentTimer.text = "Disctontent Timer: " + discontentTime;
        actionTimer.text = "Action Timer: " + actionTime;
    }

    public void UpdateGoals(Goal[] goals)
    {
        string displayText = "";

        foreach (Goal goal in goals)
        {
            if (goal is Eat)
            {
                displayText += "Eat: " + goal.value + "\n";
            }
            else if (goal is Bored)
            {
                displayText += "Bored: " + goal.value + "\n";
            }
            else if (goal is Bathroom)
            {
                displayText += "Bathroom: " + goal.value + "\n";
            }
        }

        goalValues.text = displayText;
    }

    public void UpdateDiscontentLabel(float value)
    {
        discontentLabel.text = "Discontent: " + value.ToString();
    }

    public void UpdateActionLabel(string ActionName)
    {
        actionLabel.text = "The AI has decided to " + ActionName;
    }
}

