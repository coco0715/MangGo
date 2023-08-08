using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal enum CardType
{
    Desc,
    Image
};

public class Card : MonoBehaviour
{
    public Animator anim;
    public GameObject border;

    public static string FRONT = "front";
    public static string BACK = "back";
    public static string CARD_PATH = "rtan";
    private static string BORDER = "Border";


    private bool isOpen = false;
    private CardType type = CardType.Image;

    public string member = "";
    public string imgType;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

        if(type == CardType.Desc)
        {
            //todo card selected to desc
            CardManager.Instance.SelectMember(member);
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
        Invoke("destroyCardInvoke", 1.0f);
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
