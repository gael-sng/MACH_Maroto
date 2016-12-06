using UnityEngine;
using System.Collections;

public static class PersistentData {

    //Every information that will be stored should have it's set/get

    /*  Highscore set/get   */
    public static void SetHighscore(float newHighscore) {
        PlayerPrefs.SetFloat("Highscore", newHighscore);
    }
    public static float GetHighscore() {
        return PlayerPrefs.GetFloat("Highscore");
    }
    public static void SetUserRankCoeficient(float newCoeficient) {
        PlayerPrefs.SetFloat("userRankCoeficient", newCoeficient);
    }
    public static float GetUserRankCoeficient() {
        return PlayerPrefs.GetFloat("userRankCoeficient");
    }

}
