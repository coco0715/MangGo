using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CardManager : MonoBehaviour
{
	public static CardManager Instance;
    
	public GameObject card;

	private const float INTERVAL = 1.4f;
	private const float START_POSITION_X = -2;
    private const float START_POSITION_Y = -4f;
    private const string CLOSE_CARDS = "CloseCards";

	private Sprite[] resources;
    private int[] indices;

    private static string[] MemberNames = { "이장원","김대열","임지연","최하나" };

    private string selectedMember = null;

    private Card firstCard = null;
    private Card secondCard = null;

    private bool isAnimationStarted = false;

    private void Awake()
    {
		if (Instance != null && Instance != this) {
			Destroy(this);
		}else
		{
            Instance = this;
        }
    }

    // Use this for initialization
    void Start()
	{
        InitCard();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void InitCard()
	{
        //TODO : 설명카드 배열작업 

        //load resources
        resources = Resources.LoadAll<Sprite>(Card.CARD_PATH);

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
            var cardGameObj = Instantiate(card, position, Quaternion.identity);
			Debug.Log($"cardObj is {cardGameObj}");
            Debug.Log($"spriteRenderer is {cardGameObj.transform.Find(Card.FRONT).GetComponent<SpriteRenderer>()}");
            Debug.Log($"resources are {resources}");
            Debug.Log($"resource is {resources[indices[i]]}");
            cardGameObj.transform.Find(Card.FRONT).GetComponent<SpriteRenderer>().sprite = resources[indices[i]];
            
        }
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

