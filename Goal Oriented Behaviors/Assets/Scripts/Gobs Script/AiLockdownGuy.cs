using UnityEngine;

public class AiLockdownGuy : MonoBehaviour
{
    Action[] myActions;
    Goal[] myGoals;

    [SerializeField]
    private float averageTimeBetweenTicks;
    [SerializeField]
    private float discontentmentThreshold;

    private float tickTime;
    private float timer = 0;

    private float discontent;

    bool midpointCheckFlag = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tickTime = averageTimeBetweenTicks;


        myGoals = new Goal[] { new Eat(), new Bored(), new Bathroom() };

        myActions = new Action[] { new BringPhoneToBathroom(), new NukeBathroom(),
                                    new MakeDinner(), new PlayVideoGames(), new EatChips(),
                                     new DrinkBeer() };
    
        RandomizeGoalValues();
        UI.instance.UpdateGoals(myGoals);
        CheckDiscontent();
        midpointCheckFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        UI.instance.UpdateTimer((int)timer);

        if (timer > tickTime / 2 && midpointCheckFlag)
        {
            midpointCheckFlag = false;
            Debug.Log("Midpoint check Discontent");
            CheckDiscontent();
            if (discontent > discontentmentThreshold)
            {
                DoSomething();
            }
        }

        if (timer > tickTime)
        {
            Debug.Log("Updating Goals");
            foreach (Goal goal in myGoals)
            {
                goal.value = goal.value + goal.getChange();

                goal.value = Mathf.Min(goal.value, 5f);

            }

            UI.instance.UpdateGoals(myGoals);
            CheckDiscontent();
            
            if (discontent > discontentmentThreshold)
            {
                DoSomething();
            }

            tickTime = Mathf.Max(6, Random.Range(-5f, 4f) + averageTimeBetweenTicks);
            timer = 0;
            midpointCheckFlag = true;
        }
    }

    private void CheckDiscontent()
    {
        discontent = 0;
        foreach(Goal goal in myGoals)
        {
            discontent += goal.GetDiscontentment(goal.value);
        }
        UI.instance.UpdateDiscontentLabel(discontent);
    }

    private void DoSomething()
    {
        Action action = GOBs.ChooseAction(myActions, myGoals);

        foreach (Goal goal in myGoals)
        {
            goal.value = goal.value + action.GetGoalChange(goal);
        }


        StartCoroutine(UI.instance.UpdateActionLabel(action.GetActionMessage()));
        UI.instance.UpdateGoals(myGoals);
        CheckDiscontent();

    }

    void RandomizeGoalValues()
    {
        foreach(Goal goal in myGoals)
        {
            goal.value = Random.Range(0, 4);
        }
    }
}
