using UnityEngine;
using System.Collections;

//Saves and loads data to disk: highscore, userRank
public static class PersistentData {

    //Every information that will be stored should have it's set/get

    /*  Highscore set/get   */
    public static void SetHighscore(int newHighscore) {
        PlayerPrefs.SetInt("Highscore", newHighscore);
    }
    public static int GetHighscore() {
        return PlayerPrefs.GetInt("Highscore");
    }
    public static void SetUserRankCoeficient(float newCoeficient) {
        PlayerPrefs.SetFloat("userRankCoeficient", newCoeficient);
    }
    public static float GetUserRankCoeficient() {
        return PlayerPrefs.GetFloat("userRankCoeficient", 1.0f);
    }

}
