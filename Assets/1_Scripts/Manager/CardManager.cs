using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{    
	//public GameObject card;
    Transform parent;

	private const float INTERVAL = 1.4f;
	private const float START_POSITION_X = -2;
    private const float START_POSITION_Y = -4f;
    private const string CLOSE_CARDS = "CloseCards";

	private Sprite[] resources;
    private int[] indices;

    private static string[] MemberNames = { "이장원","김대열","윤지연","최하나" };
    private static string[][] MemberDescs = {
        new string[] { "고양이❤️", "STPB" ,"보디빌딩"},
        new string[] { "고양이❤️", "STPB","요리" },
        new string[] { "수달❤️", "ISFP" ,"그림"},
        new string[] { "강아지❤️", "ISFP","독서" }
        //❤️
    };

    private string selectedMember = null;

    private Card memberCard = null;
    private Card firstCard = null;
    private Card secondCard = null;
    
    private bool isAnimationStarted = false;

    public void SetParent()
	{
        parent = GameObject.Find("Cards").transform;
	}

	public void InitCard()
	{
        //load resources
        resources = Resources.LoadAll<Sprite>(Card.CARD_PATH);

        //TODO : 설명카드 배열작업 
        for(int i = 0; i < MemberNames.Length; i++)
        {
            Vector3 position = new Vector3(START_POSITION_X + i *INTERVAL, 2f,0f);
            var descCard = Instantiate(Resources.Load<GameObject>("Prefabs/card"), position, Quaternion.identity);
            //descCard.transform.SetParent(parent);
            string[] desc = MemberDescs[i];
            Card descCardBehavior = descCard.GetComponent<Card>();
            descCardBehavior.SetDesc();
            //desc
            descCardBehavior.SetCardType(CardType.Desc);
            descCardBehavior.SetMember(MemberNames[i]);
            descCardBehavior.SetDescriptions(MemberDescs[i]);
        }

        //shuffle indices
        var listOfIndex = new List<int>();
        for (var i = 0; i < resources.Length; i++)
        {
            //카드는 2장이므로 2번 더함
            listOfIndex.Add(i);
            listOfIndex.Add(i);
        }

        indices = listOfIndex.OrderBy(_ => Random.Range(-1f, 1f)).ToArray();

        for (var i = 0; i < 16; i++) {
			var col = i % 4;
			var row = i / 4;
			
            var position = new Vector3(START_POSITION_X + col * INTERVAL, START_POSITION_Y + row * INTERVAL , 0f);
            var cardGameObj = Instantiate(Resources.Load<GameObject>("Prefabs/card"), position, Quaternion.identity);
            //cardGameObj.transform.SetParent(parent);
            Debug.Log($"resource is {resources[indices[i]]}");
            cardGameObj.transform.Find(Card.FRONT).GetComponent<SpriteRenderer>().sprite = resources[indices[i]];
            
        }
	}


    /// <summary>
    ///   <para>멤버카드를 선택합니다.</para>
    /// </summary>
    /// <param name="card">선택한 멤버카드</param>
    /// <returns>
    ///   <para>동작 이후에 카드가 선택된 상태인지를 반환한다.</para>
    /// </returns>
    public void SelectMemberCard(Card card)
    {
        if (memberCard == card) {
            card.ClickCard();
            memberCard = null;
        }else
        {
            memberCard = card;
        }
    }

    public bool IsSelectedMemberSameAs(Card card)
    {
        return memberCard == card;
    }

    public void SelectCard(Card card)
    {
        if(firstCard == null)
        {
            firstCard = card;
            card.AnimateOpen();
        }else if(secondCard == null)
        {
            isAnimationStarted = true;
            secondCard = card;
            secondCard.AnimateOpen();

            // TODO : matchManger 와 통신
            Invoke(CLOSE_CARDS, 1f);
        }else
        {
            //do nothing
            return;
        }
    }

    public void SelectMember(string member)
    {
        if(selectedMember == member) {
            selectedMember = null;
        }else{
            selectedMember = member;
        }
    }

    private void CloseCards()
    {
        firstCard.AnimateClose();
        secondCard.AnimateClose();
        firstCard = null;
        secondCard = null;
        isAnimationStarted = false;
    }

    public bool IsAnimationStarted()
    {
        return isAnimationStarted;
    }
}

