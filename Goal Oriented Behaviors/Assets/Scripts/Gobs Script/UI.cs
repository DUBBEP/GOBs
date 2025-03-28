using UnityEngine;
using TMPro;
using System.Collections;

public class UI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timer;

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
    public void UpdateTimer(float time)
    {
        timer.text = "Time: " + time;
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

    public IEnumerator UpdateActionLabel(string ActionName)
    {
        actionLabel.text = "The AI has decided to " + ActionName;
        yield return new WaitForSeconds(3f);
    }
}

