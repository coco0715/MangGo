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
        GetButton((int)Buttons.EndButton).gameObject.BindEvent(() => Application.Quit());

        if(PlayerPrefs.HasKey("firstScore"))
        {
            GetText((int)Texts.FirstScoreText).text = PlayerPrefs.GetInt("firstScore").ToString();           
        }
        if (PlayerPrefs.HasKey("secondScore"))
        {
            GetText((int)Texts.SecondScoreText).text = PlayerPrefs.GetInt("secondScore").ToString();
        }
        if (PlayerPrefs.HasKey("thirdScore"))
        {
            GetText((int)Texts.ThirdScoreText).text = PlayerPrefs.GetInt("thirdScore").ToString();
        }

        // Sound
        Managers.Sound.Clear();
        //Managers.Sound.Play("LobbyBgm", Define.Sound.Bgm);

        return true;
    }
}