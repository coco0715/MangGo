using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Main : UI_Scene
{
    float time = 30.0f;
    enum Texts
    {
        timeText,
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        time -= Time.deltaTime;
        GetText((int)Texts.timeText).text = time.ToString("N2");

        if (time < 0.0f)
        {
            time = 0;
            Managers.UI.ShowPopupUI<UI_Result>();
            Time.timeScale = 0.0f;
        }
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));

        //Managers.CardMng.SetParent();
        Managers.CardMng.InitCard();

        // Sound
        Managers.Sound.Clear();
        //Managers.Sound.Play("LobbyBgm", Define.Sound.Bgm);

        return true;
    }
}
