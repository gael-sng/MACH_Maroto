using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class displayScoreDifficulty : MonoBehaviour {

    public Text score;
    public Text highScore;
    public Text difficulty;
    public Text highDifficulty;
    private float aux;

    // Use this for initialization
    void Start () {

        score.text = "Score: " + string.Format("{0:0.00}", ScoreSystem.getPoint());
        highScore.text = "High Score: " + string.Format("{0:0.00}", ScoreSystem.GetHighscore());
        difficulty.text = "Difficulty: " + string.Format("{0:0.00}", ScoreSystem.GetUserRankCoeficient());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
