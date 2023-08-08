using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal enum CardType
{
    Desc,
    Image
};

public class Card : MonoBehaviour
{
    public Animator anim;
    public GameObject border;
    public Text desc;

    public static string FRONT = "front";
    public static string BACK = "back";
    public static string DESC = "Desc";
    public static string CARD_PATH = "rtan";
    private static string BORDER = "Border";


    private bool isOpen = false;
    internal CardType cardType { get; private set; } = CardType.Image; 

    public string member = "";
    public string imgType = "";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void SetCardType(CardType cardType)
    {
        this.cardType = cardType;
        transform.Find(FRONT).gameObject.SetActive(false);
        transform.Find(BACK).gameObject.SetActive(false);
        transform.Find(DESC).gameObject.SetActive(true);
    }

    public void SetMember(string member)
    {
        this.member = member;
    }

    public void SetDescriptions(string[] descs)
    {
        string text = member+"\n";
        foreach(string desc in descs)
        {
            text += desc+"\n";
        }
        
        desc.text = text;
        Debug.Log($"text:{text}");
    }

    public void UnSelectCard()
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
            }
            
            return;
        }

        CardManager.Instance.SelectCard(this);
        /* todo
         * 1. 카드 열었을 경우 선택된 카드가 있는지 ...??
         * 2. 카드 선택된 카드와 비교는 내가 안함 
         */
    }

    public void AnimateOpen()
    {
        isOpen = true;
        anim.SetBool("isOpen", true);
        transform.Find(FRONT).gameObject.SetActive(isOpen);
        transform.Find(BACK).gameObject.SetActive(!isOpen);
    }

    public void destroyCard()
    {
        Invoke("destroyCardInvoke", 0.1f);
    }

    void destroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void closeCard()
    {
        Invoke("closeCardInvoke", 1.0f);
    }

    void closeCardInvoke()
    {
        isOpen = false;
        anim.SetBool("isOpen", isOpen);
        transform.Find(FRONT).gameObject.SetActive(isOpen);
        transform.Find(BACK).gameObject.SetActive(!isOpen);
    }

    public void AnimateClose()
    {
        isOpen = false;
        anim.SetBool("isOpen", isOpen);
        transform.Find(FRONT).gameObject.SetActive(isOpen);
        transform.Find(BACK).gameObject.SetActive(!isOpen);

        //border
        var isClicked = border.activeSelf;
        border.SetActive(!isClicked);
    }
}
