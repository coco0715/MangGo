using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager
{
    // TODO 지울예정
    public class Cards
    {
        public string member;
        public string imgType;
    }
    public int highestScore = 200;
    public int score = 0;
    
    private bool successMatch; // 매칭 성공여부를 저장하는 변수

    public bool GetSuccessMatch()
    {
        return successMatch;
    } 

    // TODO: Cards -> Card로 변경
    public int CheckMatch(Card card1, Card card2, Card card3)
    { 
        successMatch = false;
        if(card2.imgType ==  card3.imgType)
        {
            if(card1.member == card2.member)
            {
                successMatch = true;
                return 0;
            }
            else
                return 1;
        }
        else
        {
            if(card1.member == card2.member || card1.member == card3.member)
                return 2;
            else
                return 3;
        }
    }
}
