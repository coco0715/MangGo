using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Result : UI_Popup
{
    bool isHighest = false;
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
            Managers.User.score = 0;
            SceneManager.LoadScene(Managers.Scene.GetSceneName(Managers.Scene.CurrentSceneType));
            Time.timeScale = 1;
        });
        GetButton((int)Buttons.QuitButton).gameObject.BindEvent(() => Managers.Scene.ChangeScene(Define.Scene.LobbyScene));
        SetRewardObjects(false);

        ShowResult();

        GetComponent<Canvas>().sortingOrder = 10;

        Time.timeScale = 0;
        // Sound
        //Managers.Sound.Play("DefeatEff");

        return true;
    }

    void SetRewardObjects(bool active)
    {
        GetObject((int)GameObjects.CrownImage).SetActive(active);
        GetObject((int)GameObjects.MangoIceImage).SetActive(active);
    }

    void ShowResult()
    {
        int highestScore = Managers.User.highestScore;
        int score =  Managers.User.score;

        if(highestScore <= score)
        {
            highestScore = score;
            isHighest = true;
        }
        Managers.User.AddRecord();
        Managers.User.SaveScores();
        GetText((int)Texts.FirstScoreText).text = highestScore.ToString();
        GetText((int)Texts.ScoreText).text = score.ToString();
        if(isHighest)
        {
            CoroutineHelper.StartCoroutine(ShowMango());
        }
    }

    IEnumerator ShowMango()
    {
        GetButton((int)Buttons.RetryButton).gameObject.SetActive(false);
        GetButton((int)Buttons.QuitButton).gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(2.0f);
        SetRewardObjects(true);
        GetButton((int)Buttons.RetryButton).gameObject.SetActive(true);
        GetButton((int)Buttons.QuitButton).gameObject.SetActive(true);
    }
}
