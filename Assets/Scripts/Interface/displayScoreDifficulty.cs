using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Script used for game over menu. Displays player statistics
public class displayScoreDifficulty : MonoBehaviour {

    public Text score;
    public Text highScore;
    public Text difficulty;
    public Text highDifficulty;
    private float aux;

    // Use this for initialization
    void Start () {

        ScoreSystem.MatchDetails lastMatch = ScoreSystem.GetLastMatchDetails();

        score.text = "Score: " + lastMatch.score;
        highScore.text = "High Score: " + ScoreSystem.GetHighscore();
        difficulty.text = "Difficulty: " + string.Format("{0:0.00}", lastMatch.battleCoeficient);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
