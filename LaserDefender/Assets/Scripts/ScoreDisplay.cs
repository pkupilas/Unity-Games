using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    void Start ()
	{
	    var myText = GetComponent<Text>();
	    myText.text = ScoreKeeper.GameScore.ToString();
        ScoreKeeper.ResetScore();
	}
}
