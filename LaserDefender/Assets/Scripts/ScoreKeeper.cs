using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    private Text _myText;
    private static int _score;

    public static int GameScore => _score;

    void Start()
    {
        _myText = GetComponent<Text>();
        ResetScore();
    }

    public void Score(int newPoints)
    {
        _score += newPoints;
        _myText.text = _score.ToString();
        
    }

    public static void ResetScore()
    {
        _score = 0;
    }
}
