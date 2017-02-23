using UnityEngine;
using System.Collections;

public class ActionMaster
{

    public enum Action {Tidy, Reset, EndTurn, EndGame}

    private int[] bowls = new int[21];
    private int bowlNumber = 1;

    public Action Bowl(int pins)
    {
        if (pins < 0 || pins > 10)
        {
            throw new UnityException("Pin count cannot be less than 0 and bigger than 10");
        }

        bowls[bowlNumber - 1] = pins;

        if (bowlNumber == 21 || bowlNumber == 20 && !Bowl21Awarded())
        {
            return Action.EndGame;
        }

        if (bowlNumber >= 19 && pins == 10)
        {
            bowlNumber++;
            return Action.Reset;
        }

        if (bowlNumber == 20)
        {
            bowlNumber++;

            if (bowls[19 - 1] == 10 && bowls[20 - 1] == 0)
            {
                return Action.Tidy;
            }
            if (bowls[19 - 1] + bowls[20 - 1] == 10)
            {
                return Action.Reset;
            }
            if (Bowl21Awarded())
            {
                return Action.Tidy;
            }
            
            return Action.EndGame;
        }

        if (bowlNumber % 2 != 0)
        {
            if (pins == 10)
            {
                bowlNumber += 2;
                return Action.EndTurn;
            }

            bowlNumber++;
            return Action.Tidy;
        }

        if (bowlNumber % 2 == 0)
        {
            bowlNumber++;
            return Action.EndTurn;
        }

        throw new UnityException("Not sure what action to return.");
    }

    private bool Bowl21Awarded()
    {
        return bowls[19 - 1] + bowls[20 - 1] >= 10;
    }
}
