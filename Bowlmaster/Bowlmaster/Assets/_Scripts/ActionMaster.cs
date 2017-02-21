using UnityEngine;
using System.Collections;

public class ActionMaster {

    public enum Action {Tidy, Reset, EndTurn, EndGame}

    public Action Bowl(int pins)
    {
        if (pins < 0 || pins>10)
        {
            throw new UnityException("Pin count cannot be less than 0 and bigger than 10");
        }

        if (pins == 10)
        {
            return Action.EndTurn;
        }
        throw new UnityException("Not sure what action to return.");
    }
}
