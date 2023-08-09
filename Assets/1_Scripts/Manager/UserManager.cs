using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager
{
    public int score = 0;
    public bool isContinuous = false;

    public void SetScore(int newScore)
    {
        score = newScore;
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
}