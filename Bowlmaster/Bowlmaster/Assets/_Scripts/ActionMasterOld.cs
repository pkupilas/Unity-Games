using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionMasterOld
{

    //public enum Action {Tidy, Reset, EndTurn, EndGame}

    //private int[] bowls = new int[21];
    //private int _bowlNumber = 1;

    //public static Action NextAction(List<int> pinFalls)
    //{
    //    var actionMaster = new ActionMaster();
    //    var action = new Action();

    //    foreach (var pinFall in pinFalls)
    //    {
    //        action = actionMaster.Bowl(pinFall);
    //    }

    //    return action;
    //}
    
    //private Action Bowl(int pins)
    //{
    //    if (pins < 0 || pins > 10)
    //    {
    //        throw new UnityException("Pin count cannot be less than 0 and bigger than 10");
    //    }

    //    bowls[_bowlNumber - 1] = pins;

    //    if (_bowlNumber == 21 || _bowlNumber == 20 && !Bowl21Awarded())
    //    {
    //        return Action.EndGame;
    //    }

    //    if (_bowlNumber >= 19 && pins == 10)
    //    {
    //        _bowlNumber++;
    //        return Action.Reset;
    //    }

    //    if (_bowlNumber == 20)
    //    {
    //        _bowlNumber++;

    //        if (bowls[19 - 1] == 10 && bowls[20 - 1] == 0)
    //        {
    //            return Action.Tidy;
    //        }
    //        if (bowls[19 - 1] + bowls[20 - 1] == 10)
    //        {
    //            return Action.Reset;
    //        }
    //        if (Bowl21Awarded())
    //        {
    //            return Action.Tidy;
    //        }
            
    //        return Action.EndGame;
    //    }

    //    if (_bowlNumber % 2 != 0)
    //    {
    //        if (pins == 10)
    //        {
    //            _bowlNumber += 2;
    //            return Action.EndTurn;
    //        }

    //        _bowlNumber++;
    //        return Action.Tidy;
    //    }

    //    if (_bowlNumber % 2 == 0)
    //    {
    //        _bowlNumber++;
    //        return Action.EndTurn;
    //    }

    //    throw new UnityException("Not sure what action to return.");
    //}

    //private bool Bowl21Awarded()
    //{
    //    return bowls[19 - 1] + bowls[20 - 1] >= 10;
    //}
}
