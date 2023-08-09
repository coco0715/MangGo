using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    // �ʿ��� ����1 score�� ����: ������� ���� ������ �����ϴ� ������ int���� �����Ѵ�. �� ������ �ٸ� ��ũ��Ʈ���� ����� �� �ִ�.
    // �� ������ �⺻������ 0�� �ʱⰪ���� ������.
    public int score = 0;
    // �ʿ��� ����2 isContinuous�� ����: ����ڰ� ���� �������� ������ ���� �������� �ƴ����� �����ϴ� ������ bool���� �����Ѵ�. �� ������ �ٸ� ��ũ��Ʈ���� ����� �� �ִ�.
    // �� ������ �⺻������ false�� �ʱⰪ���� ������.
    public bool isContinuous = false;

    // ��ȯ��: ����
    // �Է°�: int newScore / ����: ���ο� ���� ������ �� ����
    // �Լ���: SetScore
    // �Լ� ����: ���� ������ �Է� ���� ������ ���� �Ѵ�.
    // �� �Լ��� �ٸ� ��ũ��Ʈ���� ����� �� �ִ�.
    public void SetScore(int newScore)
    {
        score = newScore;
    }

    // ��ȯ��: ����
    // �Է°�: int losing / ����: ������ �Ұ� �� ����
    // �Լ���: LoseScore
    // �Լ� ����: ���� ������ �Է� ���� ������ŭ �����ؼ� �����Ѵ�.
    // �� �Լ��� �ٸ� ��ũ��Ʈ���� ����� �� �ִ�.
    public void LoseScore(int losing)
    {
        score -= losing;
    }

    // ��ȯ��: ����
    // �Է°�: ����
    // �Լ���: AddScore
    // �Լ� ����: �⺻������ ������ ���� 5���� ���������ְ�, ������ �������� ������ ���߰� �ִ� ���¶�� 3���� �� ���������ش�.
    // �� �Լ��� �ٸ� ��ũ��Ʈ���� ����� �� �ִ�.
    public void AddScore()
    {
        // �Լ� ������ �����ϵ��� �ڵ�¥��
        score += 5;

        if (isContinuous == true)
        {
            score += 3;
        }
    }
}