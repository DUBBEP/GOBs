
public class Eat : Goal
{
    public override float getChange()
    {
        return 1.5f;
    }

    public override float GetDiscontentment(float newValue)
    {
        return newValue * newValue;
    }
}

public class Bored : Goal
{
    public override float getChange()
    {
        return 1.7f;
    }

    public override float GetDiscontentment(float newValue)
    {
        return newValue * newValue + newValue;
    }
}

public class Bathroom : Goal
{
    public override float getChange()
    {
        return 1f;
    }

    public override float GetDiscontentment(float newValue)
    {
        return newValue * newValue;
    }
}


public class BringPhoneToBathroom : Action
{
    public override float GetDuration()
    {
        return 0.4f;
    }

    public override float GetGoalChange(Goal goal)
    {
        if (goal is Bored)
        {
            return -1;
        }
        else if (goal is Bathroom)
        {
            return -2;
        }

        return 0f;
    }

    public override string GetActionMessage()
    {
        return "Watch videos in Bathroom \n" +
            "Bored - 1 \n" +
            "Bathroom - 2 \n" +
            "time: 0.25";
    }
}

public class EatChips : Action
{
    public override float GetDuration()
    {
        return 0.2f;
    }

    public override float GetGoalChange(Goal goal)
    {
        if (goal is Eat)
        {
            return -2f;
        }

        return 0;
    }

    public override string GetActionMessage()
    {
        return "Eat Chips \n" +
            "Eat - 2.5 \n" +
            "Time: 0.2";
    }
}

public class PlayVideoGames : Action
{
    public override float GetDuration()
    {
        return 0.6f;
    }

    public override float GetGoalChange(Goal goal)
    {
        if (goal is Bored)
        {
            return -4;
        }

        return 0;
    }
    public override string GetActionMessage()
    {
        return "Play Video Games \n" +
            "Bored - 5\n" +
            "time: 0.6";
    }
}

public class MakeDinner : Action
{
    public override float GetDuration()
    {
        return 0.5f;
    }

    public override float GetGoalChange(Goal goal)
    {
        if ( goal is Eat)
        {
            return -4;
        }

        return 0;
    }
    public override string GetActionMessage()
    {
        return "Make Dinner \n" +
            "Eat - 4\n" +
            "Time: 0.5";
    }
}

public class NukeBathroom : Action
{
    public override float GetDuration()
    {
        return 0.6f;
    }

    public override float GetGoalChange(Goal goal)
    {
        if (goal is Bathroom)
        {
            return -4;
        }
        else if ( goal is Bored)
        {
            return -1;
        }

        return 0;
    }
    public override string GetActionMessage()
    {
        return "Nuke The Bathroom \n" +
            "Bathroom - 4\n" +
            "Bored - 1 \n" +
            "Time: 0.6";
    }
}

public class DrinkBeer : Action
{
    public override float GetDuration()
    {
        return 0.2f;
    }

    public override float GetGoalChange(Goal goal)
    {
        if (goal is Eat)
        {
            return -1;
        }
        else if (goal is Bored)
        {
            return -2;
        }
        else if (goal is Bathroom)
        {
            return +1;
        }

        return 0;
    }
    public override string GetActionMessage()
    {
        return "Drink Beer\n" +
            "Eat - 1\n" +
            "Bored - 3 \n" +
            "Bathroom + 1\n" +
            "Time: 0.2";
    }
}
