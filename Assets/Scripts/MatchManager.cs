using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    // TODO 지울예정
    public class Cards
    {
        public string member;
        public string imgType;
    }
    private bool successMatch; // 매칭 성공여부를 저장하는 변수

    public bool GetSuccessMatch()
    {
        return successMatch;
    } 

    // TODO: Cards -> Card로 변경
    public int CheckMatch(Cards card1, Cards card2, Cards card3)
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
            if(card1.member == card2.member)
                return 2;
            else
                return 3;
        }
    }
}
