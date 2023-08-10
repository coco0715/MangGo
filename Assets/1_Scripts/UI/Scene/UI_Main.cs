using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Main : UI_Scene
{
    public float time = 30.0f;
    enum Texts
    {
        timeText,
        scoreText,
        CheckText,
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        time -= Time.deltaTime;
        GetText((int)Texts.timeText).text = time.ToString("N2");

        if(time < 10.0f)
        {
            GetText((int)Texts.timeText).color = Color.red;
        }

        if (time <= 0.0f)
        {
            time = 0f;
            GetText((int)Texts.timeText).text = "0";
            Managers.UI.ShowPopupUI<UI_Result>();
            Time.timeScale = 0.0f;
        }
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));

        time = 30.0f;
        Managers.User.score = 0;

        GetText((int)Texts.CheckText).gameObject.SetActive(false);
        GetText((int)Texts.scoreText).text = Managers.User.score.ToString();

        Time.timeScale = 1f;

       // Sound
       Managers.Sound.Clear();
        //Managers.Sound.Play("LobbyBgm", Define.Sound.Bgm);

        return true;
    }

    public void UpdateScoreText()
    {
        GetText((int)Texts.scoreText).text = Managers.User.score.ToString();
    }

    public void ShowCheckText(string content)
    {
        CoroutineHelper.StartCoroutine(CoCheckText(content));
    }

    IEnumerator CoCheckText(string content)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.7f);
        GetText((int)Texts.CheckText).gameObject.SetActive(true);
        GetText((int)Texts.CheckText).text = content;
        yield return waitForSeconds;
        GetText((int)Texts.CheckText).gameObject.SetActive(false);
    }
}
