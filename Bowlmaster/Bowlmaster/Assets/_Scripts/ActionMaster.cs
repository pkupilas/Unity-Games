using UnityEngine;
using System.Collections;

public class ActionMaster {

    public enum Action {Tidy, Reset, EndTurn, EndGame}

    private int bowlNumber = 1;

    public Action Bowl(int pins)
    {
        if (bowlNumber == 21)
        {
            return Action.EndGame;
        }

        if (pins < 0 || pins>10)
        {
            throw new UnityException("Pin count cannot be less than 0 and bigger than 10");
        }

        if (pins == 10)
        {
            bowlNumber += 2;
            return Action.EndTurn;
        }

        if (bowlNumber % 2 != 0)
        {
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
}
