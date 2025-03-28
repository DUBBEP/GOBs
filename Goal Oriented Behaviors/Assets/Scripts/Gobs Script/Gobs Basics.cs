
public abstract class Goal
{
    public float value;

    public abstract float GetDiscontentment(float newValue);

    public abstract float getChange();
}

public abstract class Action
{
    public abstract float GetGoalChange(Goal goal);

    public abstract float GetDuration();

    public abstract string GetActionMessage();
}

public static class GOBs
{
    public static Action ChooseAction(Action[] actions, Goal[] goals)
    {
        Action bestAction = null;
        float bestValue = float.PositiveInfinity;

        foreach (Action action in actions)
        {
            float currentValue = discontentment(action, goals);

            if (currentValue < bestValue)
            {
                bestValue = currentValue;
                bestAction = action;
            }
        }

        return bestAction;
    }

    static float discontentment(Action action, Goal[] goals)
    {
        float discontentment = 0;

        foreach (Goal goal in goals)
        {
            float newValue = goal.value + action.GetGoalChange(goal);

            newValue += action.GetDuration() * goal.getChange();

            discontentment += goal.GetDiscontentment(newValue);
        }

        return discontentment;
    }
}
