using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class displayScoreDifficulty : MonoBehaviour {

    public Text score;
    public Text highScore;
    public Text difficulty;
    public Text highDifficulty;

    // Use this for initialization
    void Start () {
        score.text = "Score: " + ScoreSystem.getPoint();
        highScore.text = "High Score: " + ScoreSystem.GetHighscore();
        difficulty.text = "Difficulty: " + ScoreSystem.GetUserRankCoeficient();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
