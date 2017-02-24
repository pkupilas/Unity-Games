using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreMaster {

    private const int _STRIKE = 10;

    public static List<int> ScoreCumulative(List<int> rolls)
    {
        var cumulativeScoresList = new List<int>();
        bool lastRollWasStrike = false;
        var scoreFrames = ScoreFrames(rolls);
        int sum = 0;

        foreach (var score in scoreFrames)
        {
            sum += score;
            cumulativeScoresList.Add(sum);
        }

        return cumulativeScoresList;
    }


    public static List<int> ScoreFrames(List<int> rolls)
    {
        var frameList = new List<int>();
        int firstScoreFrame = 0;
        int secondScoreFrame = 0;

        foreach (var roll in rolls)
        {
            if (roll == _STRIKE)
            {
                frameList.Add(_STRIKE);
            }
            else if(firstScoreFrame==0)
            {
                firstScoreFrame = roll;
            }
            else if(secondScoreFrame==0)
            {
                secondScoreFrame = roll;
                frameList.Add(firstScoreFrame+secondScoreFrame);
                firstScoreFrame = 0;
                secondScoreFrame = 0;
            }
        }

        return frameList;
    }

}
