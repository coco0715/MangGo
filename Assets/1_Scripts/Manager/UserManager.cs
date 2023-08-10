using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager
{
    public int highestScore = 0;
    public int secondScore = 0;
    public int thirdScore = 0;
    public int score = 0;
    public List<int> record = new List<int>();
    public bool isContinuous = false;

    public void InitUserHighestScore()
    {
        if(PlayerPrefs.HasKey("highestScore"))
        {
            highestScore = PlayerPrefs.GetInt("highestScore");
        }
    }

    public void LoseScore(int losing)
    {
        score -= losing;
        if(score <= 0) 
        {
            score = 0;
        }
    }

    public void AddScore()
    {
        score += 5;

        if (isContinuous == true)
        {
            score += 3;
        }
    }

    public void SetScores()
    {
        if (highestScore <= score)
        {
            highestScore = score;
        }
        else if(secondScore <= score)
        {
            secondScore = score;
        }
        else if(thirdScore <= score)
        {
            thirdScore = score;
        }

        SaveScores();
    }

    public void UpdateRecord()
    {
        if (!record.Contains(highestScore))
        {
            record.Add(highestScore);
            record.Sort();
        }

        if (!record.Contains(score))
        {
            record.Add(score);
            record.Sort();
        }        
    }

    public void SaveScores()
    {
        PlayerPrefs.SetInt("firstScore", highestScore);
        PlayerPrefs.SetInt("secondScore", secondScore);
        PlayerPrefs.SetInt("thirdScore", thirdScore);
    }
}