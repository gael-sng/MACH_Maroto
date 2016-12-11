using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Script used for game over menu. Displays player statistics
public class displayScoreDifficulty : MonoBehaviour {

    public Text score;
    public Text highScore;
    public Text difficulty;
    public Text newDifficuty;
    private float aux;

    // Use this for initialization
    void Start () {

        ScoreSystem.MatchDetails lastMatch = ScoreSystem.GetLastMatchDetails();

        score.text += lastMatch.score;
        highScore.text += ScoreSystem.GetHighscore();
        difficulty.text += lastMatch.battleCoeficient.ToString("F2");
        float difference = ScoreSystem.GetUserRankCoeficient() - lastMatch.battleCoeficient;
        newDifficuty.text += ScoreSystem.GetUserRankCoeficient().ToString("F2");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
