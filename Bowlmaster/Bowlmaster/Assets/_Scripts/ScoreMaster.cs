using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreMaster {
    
    public static List<int> ScoreCumulative(List<int> rolls)
    {
        var cumulativeScoresList = new List<int>();
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
        var frames = new List<int>();

        for (int i = 1; i < rolls.Count; i+=2)
        {
            if (frames.Count == 10) break;

            //NORMAL FRAME
            if (rolls[i] + rolls[i - 1] < 10)
            {
                frames.Add(rolls[i] + rolls[i - 1]);
            }

            if (i + 1 == rolls.Count) break;

            //SPARE BONUS
            if (rolls[i] + rolls[i - 1] == 10)
            {
                frames.Add(10+rolls[i+1]);
            }
            else if (rolls[i - 1] == 10) // STRIKE BONUS
            {
                i--;                     // modify i due to strike bonus (i+2)
                frames.Add(10 + rolls[i + 1] + rolls[i + 2]);
            }
        }
         
        return frames;
    }

}
