using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager
{
    public int highestScore = 0;
    public int score = 0;
    public List<int> record = new List<int>();
    public bool isContinuous = false;

    public void InitScores()
    {
        string[] scores = PlayerPrefs.GetString("scores", "0,0,0").Split(',');
        for(int i = 0; i < scores.Length; i++)
        {
            int tmp;
            Int32.TryParse(scores[i], out tmp);
            record.Add(tmp);
        }
        highestScore = int.Parse(scores[0]);
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

    public void AddRecord()
    {
        if (!record.Contains(highestScore))
        {
            record.Add(highestScore);
        }

        if (!record.Contains(score))
        {
            record.Add(score);
        }
        record.Sort(new Comparison<int>((n1, n2) => n2.CompareTo(n1)));
    }

    public void SaveScores()
    {
        string scores = "";
    
        for (int i = 0; i < 3; i++)
        {
            if(record[i] == null)
            {
                scores += "0,";
            }
            else
            {
                scores += record[i].ToString() + ",";
            }
            
        }
        
        PlayerPrefs.SetString("scores", scores);
    }
}