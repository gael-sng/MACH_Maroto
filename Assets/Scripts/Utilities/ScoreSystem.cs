﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ScoreSystem {

    private static float point;

    private static readonly float TIME_COEFICIENT = 1 / 90.0f;
    private static readonly float KILLS_COEFICIENT = 1 / 30.0f;
    private static readonly float EXPECTED_TIME = 90.0f; //60 secs
    private static readonly int EXPECTED_KILLS = 30; //20 kills
    private static readonly float DEFAULT_SCORE = EXPECTED_TIME * TIME_COEFICIENT + EXPECTED_KILLS * KILLS_COEFICIENT; //Default score means the player stayed alive for the expectedTime, and got the expectedKills

    private static readonly int MATCH_HISTORY_STORE_COUNT = 10;
    private static readonly float BONUS_COEFICIENT = 0.20f;
    private static readonly float NORMAL_COEFICIENT = 0.10f;
    private static readonly float MIN_RANK_COEFICIENT = 0.10f;
    private static readonly float MAX_RANK_COEFICIENT = Mathf.Infinity;

    //Calculates the match score due to the userRankCoeficient, and update it, as well as the match history
    private static float CalculateMatchScore(float time, int kills) {
        float userRankCoeficient = GetUserRankCoeficient(); //Loads the userRankCoeficient (Gets 1.0 if new player)
        Queue<float> matchHistory = LoadMatchHistory(); //Loads the match history

        //Calculate the rawMatchScore (Ignores the userRankCoeficient)
        float rawMatchScore = (DEFAULT_SCORE + (time - EXPECTED_TIME) * TIME_COEFICIENT + (kills - EXPECTED_KILLS) * KILLS_COEFICIENT);

        float increment;
        //Calculate userMatchScore
        if (GetAverageMatchResults(matchHistory) >= userRankCoeficient) {
            if (rawMatchScore >= userRankCoeficient) {
                //Gets extra scrore
                increment = (rawMatchScore - userRankCoeficient) * BONUS_COEFICIENT;
            } else {
                //Looses normal score
                increment = (rawMatchScore - userRankCoeficient) * NORMAL_COEFICIENT;
            }
        } else {
            if (rawMatchScore >= userRankCoeficient) {
                //Gets normal score
                increment = (rawMatchScore - userRankCoeficient) * NORMAL_COEFICIENT;
            } else {
                //Looses extra score
                increment = (rawMatchScore - userRankCoeficient) * BONUS_COEFICIENT;
            }
        }

        //Calculates the score due to the userRankCoeficient
        float finalMatchScore = rawMatchScore * userRankCoeficient;

        //Adds the match registry to the match history
        AddMatch(finalMatchScore, matchHistory);

        //Saves the new userRankCoeficient, which value must be between MIN_RANK_COEFICIENT and MAX_RANK_COEFICIENT
        PersistentData.SetUserRankCoeficient(Mathf.Clamp(userRankCoeficient + increment, MIN_RANK_COEFICIENT, MAX_RANK_COEFICIENT));

        return finalMatchScore;
    }

    //Adds a new match registry to the matchHistory list
    private static void AddMatch(float newMatchScore, Queue<float> matchHistory) {

        //Adds this match results to the matchHistory
        if (matchHistory.Count >= MATCH_HISTORY_STORE_COUNT) {
            matchHistory.Dequeue();
        }
        matchHistory.Enqueue(newMatchScore);

        //Saves the new matchHistory to the disk
        SaveMatchHistory(matchHistory);

        //Also checks if it is a new highscore, and update it
        if (newMatchScore > GetHighscore()) {
            PersistentData.SetHighscore(newMatchScore);
        }
    }

    //Gets the average match results in the matchHistory list
    private static float GetAverageMatchResults(Queue<float> matchHistory) {
        float sum = 0.0f;

        foreach (float match in matchHistory) {
            sum += match;
        }
        return sum / matchHistory.Count;
    }

    //Saves the matchHistory to the disk
    private static void SaveMatchHistory(Queue<float> matchHistory) {
        for (int i = 0; i < MATCH_HISTORY_STORE_COUNT; i++) {
            if (PlayerPrefs.HasKey("MatchHistory(" + i + ")"))
                PlayerPrefs.SetFloat("MatchHistory(" + i + ")", matchHistory.ToArray()[i]);
            else
                break;
        }
    }

    //Loads the matchHistory from the disk
    private static Queue<float> LoadMatchHistory() {

        Queue<float> newQueue = new Queue<float>();

        for (int i = 0; i < MATCH_HISTORY_STORE_COUNT; i++) {
            float value = PlayerPrefs.GetFloat("MatchHistory(" + i + ")", -1.0f);

            if (value == -1.0f)
                break;
            else
                newQueue.Enqueue(value);
        }

        return newQueue;
    }

    public static float getPoint()
    {
        return point;
    }

    //Receives a new match result
    public static float ReceiveMatchResults(float time, int kills) {
        point = CalculateMatchScore(time, kills);
        return point;
    }

    //Returns the user Rank Coeficient
    public static float GetUserRankCoeficient() {
        return PersistentData.GetUserRankCoeficient();
    }

    //Returns the user Highscore
    public static float GetHighscore() {
        return PersistentData.GetHighscore();
    }

    public static float getMasterVolume()
    {
        return PlayerPrefs.GetFloat("master_volume");
    }

}