using System;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

internal enum CardType
{
    Desc,
    Image
}

public class Card : MonoBehaviour
{
    public Animator anim;
    public GameObject border;
    public Text desc;
    public GameObject progressBox;

    public static string FRONT = "front";
    public static string BACK = "back";
    public static string DESC = "Desc";
    public static string CARD_PATH = "Sprites/member";
    private static string BORDER = "Border";

    private int progress;

    private bool isOpen;
    internal CardType cardType { get; private set; } = CardType.Image;

    public string member = "";
    public string imgType = "";


    internal void SetCardType(CardType cardType)
    {
        this.cardType = cardType;
        transform.Find(FRONT).gameObject.SetActive(false);
        transform.Find(BACK).gameObject.SetActive(false);

        switch (cardType)
        {
            case CardType.Desc:
                break;
            case CardType.Image:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(cardType), cardType, null);
        }

        transform.Find(DESC).gameObject.SetActive(true);
    }

    public void SetMember(string member)
    {
        this.member = member;
    }

    public void SetDescriptions(string[] descs)
    {
        progressBox.SetActive(true);

        string text = member + "\n";
        text += $"{descs[0]}\n";

        desc.text = text;
        Debug.Log($"text:{text}", border);
    }

    public void SetBorderInactive()
    {
        border.SetActive(false);
    }

    public void ClickCard()
    {
        //animation 중에는 선택 불가능 하도록 ... firstCard, secondCard 의 참조가 꼬여서 잘못된 객체가 조작됨 
        if (CardManager.Instance.IsAnimationStarted())
        {
            return;
        }

        if (!CardManager.Instance.IsMemberCardSelected() && cardType != CardType.Desc)
        {
            //todo 멤버 카드를 먼저 선택 할 수 있도록 안내 
            return;
        }

        //border
        var isClicked = border.activeSelf;
        border.SetActive(!isClicked);

        //on Description clicked
        if (cardType == CardType.Desc)
        {
            Debug.Log("설명 카드 클릭");
            if (CardManager.Instance.IsSelectedMemberSameAs(this))
            {
                Debug.Log("같은 설명 카드 클릭");
                CardManager.Instance.UnSelectMemberCard();
            }
            else
            {
                Debug.Log("다른 설명 카드 클릭");
                CardManager.Instance.SelectMemberCard(this);
            }

            return;
        }

        if (CardManager.Instance.IsMemberCardSelected())
        {
            CardManager.Instance.SelectCard(this);
        }
        else
        {
            //show text 
            Debug.Log("멤버카드가 선택되지 않았음.");
        }
    }

    public void AnimateOpen()
    {
        isOpen = true;
        anim.SetBool("isOpen", true);
        transform.Find(FRONT).gameObject.SetActive(isOpen);
        transform.Find(BACK).gameObject.SetActive(!isOpen);
    }

    public void AnimateClose()
    {
        isOpen = false;
        anim.SetBool("isOpen", isOpen);
        transform.Find(FRONT).gameObject.SetActive(isOpen);

        var back = transform.Find(BACK).gameObject;
        var backRenderer = back.GetComponent<SpriteRenderer>();
        backRenderer.color = Color.gray.WithAlpha(alpha: 0.8f);
        back.SetActive(!isOpen);

        //border
        var isClicked = border.activeSelf;
        border.SetActive(!isClicked);
    }

    public void Fadeout()
    {
        anim.SetBool("isDestroyed", true);
    }

    public void Progress()
    {
        if (cardType != CardType.Desc)
        {
            throw new InvalidDataException("진행도 호출은 오로지 설명 카드에서만 호출.");
        }

        progress++;
        var progressFloat = progress / (float)CardManager.MaxProgress;
        Debug.Log($"progress = {progressFloat}");
        desc.text += $"{CardManager.Instance.GetDescription(member, progress)}\n";
        
        //UI update
        progressBox.transform.localScale = new Vector3(1f, progressFloat);
        if (progress == CardManager.MaxProgress)
        {
            //임시방편 
            progressBox.transform.localPosition = new Vector3(0f,0f,0f);
        }
    }
}