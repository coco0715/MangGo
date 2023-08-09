using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Result : UI_Popup
{
    enum GameObjects
    {
        CrownImage,
        MangoIceImage,
    }
    enum Texts
    {
        FirstScoreText,
        ScoreText,
    }
    enum Buttons
    {
        RetryButton,
        QuitButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.RetryButton).gameObject.BindEvent(() => {
            SceneManager.LoadScene(Managers.Scene.GetSceneName(Managers.Scene.CurrentSceneType));
            Time.timeScale = 1;
        });
        GetButton((int)Buttons.QuitButton).gameObject.BindEvent(() => Managers.Scene.ChangeScene(Define.Scene.LobbyScene));
        SetRewardObjects(false);

        StartCoroutine("ShowResult");

        Time.timeScale = 0;
        GetComponent<Canvas>().sortingOrder = 10;

        // Sound
        //Managers.Sound.Play("DefeatEff");

        return true;
    }

    void SetRewardObjects(bool active)
    {
        GetObject((int)GameObjects.CrownImage).SetActive(active);
        GetObject((int)GameObjects.MangoIceImage).SetActive(active);
    }

    IEnumerator ShowResult()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(2f);
        // TODO UserManger.user.highestScore로 수정해야 함
        int highestScore = Managers.Match.highestScore;
        // TODO UserManager.user.score로 수정해야 함
        int score =  Managers.User.score;
        bool isHighest = false;

        if(score > highestScore)
        {
            highestScore = score;
            isHighest = true;
        }
        GetText((int)Texts.FirstScoreText).text = highestScore.ToString();
        GetText((int)Texts.ScoreText).text = score.ToString();
        
        yield return waitForSeconds;

        if(isHighest)
        {
            SetRewardObjects(true);
        }
    }
}
