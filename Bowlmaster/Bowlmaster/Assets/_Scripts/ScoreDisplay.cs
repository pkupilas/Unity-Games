using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ScoreDisplay : MonoBehaviour
{

    public Text[] rollsText;
    public Text[] frameText;

    public void FillRolls(List<int> rolls)
    {
        string formattedRolls = FormatRolls(rolls);

        for (int i = 0; i < formattedRolls.Length; i++)
        {
            rollsText[i].text = formattedRolls[i].ToString();
        }
    }

    public void FillFrames(List<int> scores)
    {
        for (int i = 0; i < scores.Count; i++)
        {
            frameText[i].text = scores[i].ToString();
        }
    }

    public static string FormatRolls(List<int> rolls)
    {
        string output = "";

        for (int i = 0; i < rolls.Count; i++)
        {
            int box = output.Length + 1;

            if (rolls[i] == 0)
            {
                output += "-";                                                      // DASH AS ZERO
            }
            else if ((box % 2 == 0 || box==21) && rolls[i - 1] + rolls[i] == 10)    // SPARE AS SLASH
            {
                output += "/";
            }
            else if (box >= 19 && rolls[i] == 10)                                   // STRIKE AS X W/O SPACE WHEN IN LAST FRAME
            {
                output += "X";
            }
            else if (rolls[i]==10)                                                  // STRIKE
            {
                output += "X ";
            }
            else                                                                    // NORMAL FRAME
            {
                output += rolls[i].ToString();
            }
        }

        return output;
    }

}
