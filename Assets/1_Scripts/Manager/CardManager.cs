using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    public GameObject card;
    public static int MaxProgress = 2;

    private const float Interval = 1.4f;
    private const float StartPositionX = -2;
    private const float StartPositionY = -4f;
    private const string MethodCloseCards = "CloseCards";
    private const string MethodDestroyCards = "DestroyCards";

    private Sprite[] _resources;
    private int[] _indices;

    private static readonly string[] MemberNames = { "김대열", "윤지연", "이장원", "최하나" };

    private static readonly string[][] MemberDescs =
    {
        new string[] { "ENTP", "보디빌딩", "힘내자!", "게임", "독서", "낮잠자기", "코인", "주식" },
        new string[] { "INTP", "게임", "취업하자!", "코인", "스포츠토토", "카지노", "주식", "돈이좋아" },
        new string[] { "ENFP️", "게임", "지지말자!", "코인", "스포츠토토", "카지노", "주식", "돈이좋아" },
        new string[] { "INFJ️", "낮잠자기", "운전하기", "코인", "스포츠토토", "카지노", "주식", "돈이좋아" },
    };

    private string _selectedMember;
    private readonly List<Card> _memberCards = new();
    private Card _selectedMemberCard;
    private Card _firstCard;
    private Card _secondCard;
    private bool _isAnimationStarted;
    private int _cardCount = 16;
    private bool _isFirstlyInitCard = true;
    UI_Main mainUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        InitCard();
    }

    private void InitCard()
    {
        _cardCount = 16;
        //load resources
        _resources = Resources.LoadAll<Sprite>(Card.CARD_PATH);

        //TODO : 설명카드 배열작업 
        if (_isFirstlyInitCard)
        {
            for (var i = 0; i < MemberNames.Length; i++)
            {
                var position = new Vector3(StartPositionX + i * Interval, 2f, 0f);
                var descCard = Instantiate(card, position, Quaternion.identity);
                var descCardBehavior = descCard.GetComponent<Card>();
                //desc
                descCardBehavior.SetCardType(CardType.Desc);
                descCardBehavior.SetMember(MemberNames[i]);
                descCardBehavior.SetDescriptions(MemberDescs[i]);
                _memberCards.Add(descCardBehavior);
            }
        }

        //shuffle indices
        var listOfIndex = new List<int>();
        for (var i = 0; i < _resources.Length; i++)
        {
            //카드는 2장이므로 2번 더함
            //카드수가 변경되는 경우에 사용할 수 있도록 규칙적용  
            var repeat = _cardCount / _resources.Length;
            for (var j = 0; j < repeat; j++)
            {
                listOfIndex.Add(i);
            }
        }

        _indices = listOfIndex.OrderBy(_ => Random.Range(-1f, 1f)).ToArray();
        // _indices = listOfIndex.ToArray();

        for (var i = 0; i < 16; i++)
        {
            var col = i % 4;
            var row = i / 4;

            var position = new Vector3(StartPositionX + col * Interval, StartPositionY + row * Interval, 0f);
            var cardGameObj = Instantiate(card, position, Quaternion.identity);
            cardGameObj.transform.localScale = new Vector3(100f, 100f, 0f);
            Debug.Log($"resource is {_resources[_indices[i]]}");

            //resource 
            cardGameObj.transform.Find(Card.FRONT).GetComponent<SpriteRenderer>().sprite = _resources[_indices[i]];
            var cardData = cardGameObj.GetComponent<Card>();

            //멤버명 할당과 이미지 타입 할당.
            cardData.member = MemberNames[_indices[i] / 2];
            cardData.imgType = _indices[i] % 2 == 0 ? $"{cardData.member}A" : $"{cardData.member}B";
        }
    }

    public void SelectMemberCard(Card memberCard)
    {
        if (_selectedMemberCard == null)
        {
            _selectedMemberCard = memberCard;
        }
        else if (_selectedMemberCard == memberCard)
        {
            _selectedMemberCard.SetBorderInactive();
            _selectedMemberCard = null;
        }
        else
        {
            _selectedMemberCard.SetBorderInactive();
            _selectedMemberCard = memberCard;
        }
    }

    public void UnSelectMemberCard()
    {
        _selectedMemberCard.SetBorderInactive();
        _selectedMemberCard = null;
    }

    public bool IsSelectedMemberSameAs(Card memberCard)
    {
        return _selectedMemberCard == memberCard;
    }

    public void SelectCard(Card memberCard)
    {
        mainUI = GameObject.Find("UI_Main").GetComponent<UI_Main>();
        if (_firstCard == null)
        {
            _firstCard = memberCard;
            memberCard.AnimateOpen();
        }
        else if (_secondCard == null)
        {
            _isAnimationStarted = true;
            _secondCard = memberCard;
            _secondCard.AnimateOpen();

            // Invoke(MethodCloseCards, 1f);

            //만약 같으면 카드 파괴
            var result = Managers.Match.CheckMatch(_selectedMemberCard, _firstCard, _secondCard);
            switch (result)
            {
                case 0:
                    Managers.User.AddScore();
                    Managers.User.isContinuous = true;
                    Debug.Log("matchSuccess");
                    _firstCard.Fadeout();
                    _secondCard.Fadeout();
                    Invoke(MethodDestroyCards, 0.5f);
                    Debug.Log("매칭 성공!");
                    mainUI.ShowCheckText(memberCard.member);
                    mainUI.UpdateScoreText();
                    mainUI.time += 5f;
                    break;
                case 1:
                    Managers.User.isContinuous = false;
                    Managers.User.LoseScore(1);
                    Invoke(MethodCloseCards, 0.5f);
                    Debug.Log("그림 O, 멤버 X");
                    mainUI.ShowCheckText("실패");
                    mainUI.UpdateScoreText();
                    break;
                case 2:
                    Managers.User.isContinuous = false;
                    Managers.User.LoseScore(2);
                    Debug.Log("그림 X, 멤버 O");
                    Invoke(MethodCloseCards, 0.5f);
                    mainUI.ShowCheckText("실패");
                    mainUI.UpdateScoreText();
                    break;
                case 3:
                    Managers.User.isContinuous = false;
                    Managers.User.LoseScore(3);
                    Debug.Log("그림 X, 멤버 X");
                    Invoke(MethodCloseCards, 0.5f);
                    mainUI.ShowCheckText("실패");
                    mainUI.UpdateScoreText();
                    break;

                default: throw new InvalidDataException("결과값은 반드시 0,1,2,3 중 하나여야만 한다.");
            }
        } //else do nothing
    }

    private void CloseCards()
    {
        _firstCard.AnimateClose();
        _secondCard.AnimateClose();
        _firstCard = null;
        _secondCard = null;
        _selectedMemberCard.SetBorderInactive();
        _selectedMemberCard = null;
        _isAnimationStarted = false;
    }

    private void DestroyCards()
    {
        Destroy(_firstCard.gameObject);
        Destroy(_secondCard.gameObject);
        _firstCard = null;
        _secondCard = null;
        _selectedMemberCard.Progress();
        _selectedMemberCard.SetBorderInactive();
        _selectedMemberCard = null;
        _isAnimationStarted = false;
        _cardCount -= 2;

        if (_cardCount == 0)
        {
            _isFirstlyInitCard = false;
            _memberCards.ForEach(member => member.InitProgress());
            InitCard();
        }
    }

    public bool IsAnimationStarted()
    {
        return _isAnimationStarted;
    }

    public bool IsMemberCardSelected()
    {
        return _selectedMemberCard != null;
    }

    public string GetDescription(string member, int index)
    {
        var memberIndex = MemberNames.TakeWhile(memberName => memberName != member).Count();
        var descriptions = MemberDescs[memberIndex];
        return descriptions[index % descriptions.Length];
    }

    public void NoticeSelectMemberCardFirstly()
    {
        _memberCards.ForEach(it =>
        {
            Debug.Log($"memberCard:{it}");
            it.AnimateScaleUp();
        });
    }
}