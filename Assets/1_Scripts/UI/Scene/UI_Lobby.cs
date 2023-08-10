using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

        List<TextMeshProUGUI> scoreTxts = new List<TextMeshProUGUI>();
        scoreTxts.Add(GetText((int)Texts.FirstScoreText));
        scoreTxts.Add(GetText((int)Texts.SecondScoreText));
        scoreTxts.Add(GetText((int)Texts.ThirdScoreText));

        /*string[] scores = PlayerPrefs.GetString("scores","0,0,0").Split(',');

        for(int i = 0; i < scores.Length; i++)
        {
            Debug.Log(scores[i]);
            scoreTxts[i].text = scores[i];
        } */
        for(int i=0; i <3; i++)       
        {
            scoreTxts[i].text = Managers.User.record[i].ToString();
        }

        // Sound
        Managers.Sound.Clear();
        //Managers.Sound.Play("LobbyBgm", Define.Sound.Bgm);

        return true;
    }
}