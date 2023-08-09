using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    // 필요한 변수1 score의 역할: 사용자의 현재 점수를 저장하는 변수로 int값을 저장한다. 이 변수는 다른 스크립트에서 사용할 수 있다.
    // 이 변수는 기본적으로 0을 초기값으로 가진다.
    public int score = 0;
    // 필요한 변수2 isContinuous의 역할: 사용자가 현재 연속으로 정답을 맞춘 상태인지 아닌지를 저장하는 변수로 bool값을 저장한다. 이 변수는 다른 스크립트에서 사용할 수 있다.
    // 이 변수는 기본적으로 false를 초기값으로 가진다.
    public bool isContinuous = false;

    // 반환값: 없음
    // 입력값: int newScore / 역할: 새로운 유저 점수가 될 점수
    // 함수명: SetScore
    // 함수 역할: 유저 점수를 입력 받은 점수로 저장 한다.
    // 이 함수는 다른 스크립트에서 사용할 수 있다.
    public void SetScore(int newScore)
    {
        score = newScore;
    }

    // 반환값: 없음
    // 입력값: int losing / 역할: 유저가 잃게 될 점수
    // 함수명: LoseScore
    // 함수 역할: 유저 점수를 입력 받은 점수만큼 간점해서 저장한다.
    // 이 함수는 다른 스크립트에서 사용할 수 있다.
    public void LoseScore(int losing)
    {
        score -= losing;
    }

    // 반환값: 없음
    // 입력값: 없음
    // 함수명: AddScore
    // 함수 역할: 기본적으로 유저의 점수 5점을 득점시켜주고, 유저가 연속으로 정답을 맞추고 있는 상태라면 3점을 더 득점시켜준다.
    // 이 함수는 다른 스크립트에서 사용할 수 있다.
    public void AddScore()
    {
        // 함수 역할을 수행하도록 코드짜기
        score += 5;

        if (isContinuous == true)
        {
            score += 3;
        }
    }
}