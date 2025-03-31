using UnityEngine;

public class AiLockdownGuy : MonoBehaviour
{
    Action[] myActions;
    Goal[] myGoals;

    [SerializeField]
    private float discontentRefereshTime;
    [SerializeField]
    private float actionRefreshTime;
    [SerializeField]
    private float discontentmentThreshold;

    private float discontentTickTime;
    private float actionTickTime;
    private float discontentTimer;
    private float actionTimer;

    void Start()
    {
        discontentTickTime = discontentRefereshTime;
        actionTickTime = actionRefreshTime;

        discontentTimer = discontentTickTime;
        actionTimer = actionTickTime;


        myGoals = new Goal[] { new Eat(), new Bored(), new Bathroom() };

        myActions = new Action[] { new BringPhoneToBathroom(), new NukeBathroom(),
                                    new MakeDinner(), new PlayVideoGames(), new EatChips(),
                                     new DrinkBeer() };
    
        RandomizeGoalValues();
        UI.instance.UpdateGoals(myGoals);
        CheckDiscontent();
    }

    void Update()
    {
        discontentTimer -= Time.deltaTime;
        actionTimer -= Time.deltaTime;

        UI.instance.UpdateTimers((int)discontentTimer, (int)actionTimer);
        CheckDiscontentTimer();
        CheckActionTimer();
    }

    // check if we want to perform an action.
    private void CheckActionTimer()
    {
        if (actionTimer < 0)
        {
            if (CheckDiscontent())
                DoSomething();
            else
                UI.instance.UpdateActionLabel("do nothing.");

            actionTickTime = Mathf.Max(6, Random.Range(-3, 4) + actionRefreshTime);
            actionTimer = actionTickTime;
        }

    }

    // increase our discontent if the timer has passed the tick value
    private void CheckDiscontentTimer()
    {
        if (discontentTimer < 0)
        {
            foreach (Goal goal in myGoals)
            {
                goal.value = goal.value + goal.getChange();
                goal.value = Mathf.Min(goal.value, 5f);
            }
            UI.instance.UpdateGoals(myGoals);
            CheckDiscontent();

            discontentTickTime = Mathf.Max(6, Random.Range(-5, 5) + discontentRefereshTime);
            discontentTimer = discontentTickTime;
        }
    }


    // returns true if discontent is greater than discontent threshold. Also updates discontent UI.
    private bool CheckDiscontent()
    {
        float discontent = 0;
        foreach(Goal goal in myGoals)
        {
            discontent += goal.GetDiscontentment(goal.value);
        }
        
        UI.instance.UpdateDiscontentLabel(discontent);

        if (discontent > discontentmentThreshold)
            return true;

        return false;
    }

    // Picks the best action to take and updates goal values.
    private void DoSomething()
    {
        Action action = GOBs.ChooseAction(myActions, myGoals);

        foreach (Goal goal in myGoals)
        {
            goal.value = goal.value + action.GetGoalChange(goal);
        }

        UI.instance.UpdateActionLabel(action.GetActionMessage());
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
