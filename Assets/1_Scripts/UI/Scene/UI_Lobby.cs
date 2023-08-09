using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Lobby : UI_Scene
{
    enum Buttons
    {
        StartButton,
        EndButton,
    }

    enum Texts
    {
        FirstScoreText,
        SecondScoreText,
        ThirdScoreText,
    }

    private void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        
        GetButton((int)Buttons.StartButton).gameObject.BindEvent(() => Managers.Scene.ChangeScene(Define.Scene.MainScene));
        GetButton((int)Buttons.EndButton).gameObject.BindEvent(() => {
            Managers.Match.highestScore = 300;
            Managers.Match.score = 400;
            Managers.UI.ShowPopupUI<UI_Result>();
        });
        
        GetText((int)Texts.FirstScoreText).text = "120";
        GetText((int)Texts.SecondScoreText).text = "100";
        GetText((int)Texts.ThirdScoreText).text = "90";

        // Sound
        Managers.Sound.Clear();
        //Managers.Sound.Play("LobbyBgm", Define.Sound.Bgm);

        return true;
    }
}