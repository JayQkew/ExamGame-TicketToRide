using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrainDeckManager : MonoBehaviour, IPointerClickHandler
{
    #region CARD PILE LISTS:
    [SerializeField] List<GameObject> trainCards = new List<GameObject>();
    [SerializeField] List<GameObject> discardedTrainCards = new List<GameObject>();
    #endregion

    #region TRAIN INT VARIABLES:
    private int colourTrains = 12;
    private int locomotive = 14;
    #endregion

    #region TRAIN GAME OBJECTS VARIABLES:
    [SerializeField] GameObject redCard;
    [SerializeField] GameObject greenCard;
    [SerializeField] GameObject blueCard;
    [SerializeField] GameObject blackCard;
    [SerializeField] GameObject whiteCard;
    [SerializeField] GameObject pinkCard;
    [SerializeField] GameObject orangeCard;
    [SerializeField] GameObject yellowCard;
    [SerializeField] GameObject locomotiveCard;

    [SerializeField] GameObject trainHand;
    [SerializeField] public GameObject discardedPile;
    #endregion

    #region OTHER SCRIPTS:
    [SerializeField] TrainCard cs_trainCard;
    [SerializeField] GameObject go_trainCard;
    [SerializeField] PlayerManager cs_playerManager1;
    [SerializeField] GameObject go_playerManager1;
    [SerializeField] PlayerManager cs_playerManager2;
    [SerializeField] GameObject go_playerManager2;
    [SerializeField] MarketManager cs_marketManager;
    [SerializeField] GameObject go_marketManager;
    [SerializeField] ActionManager cs_actionManager;
    #endregion

    #region OTHER VARIABLES:
    [SerializeField] public bool canDrawTrainCard;
    [SerializeField] public int cardPickUpCounter;
    #endregion

    void Awake()
    {
        #region GETTING OTHER SCRIPTS:
        cs_playerManager1 = go_playerManager1.GetComponent<PlayerManager>();
        cs_playerManager2 = go_playerManager2.GetComponent<PlayerManager>();
        cs_trainCard = go_trainCard.GetComponent<TrainCard>();
        cs_marketManager = go_marketManager.GetComponent<MarketManager>();
        #endregion

        #region ADDING CARDS:
        AddingCards(colourTrains, redCard);
        AddingCards(colourTrains, greenCard);
        AddingCards(colourTrains, blueCard);
        AddingCards(colourTrains, blackCard);
        AddingCards(colourTrains, whiteCard);
        AddingCards(colourTrains, pinkCard);
        AddingCards(colourTrains, orangeCard);
        AddingCards(colourTrains, yellowCard);
        AddingCards(locomotive, locomotiveCard);
        #endregion

        canDrawTrainCard = true;

    }

    private void Start()
    {
        for (int i = 4; i > 0; i--)
        {
            DrawCard(Vector3.zero, go_playerManager1.transform, "trainCard");
            DrawCard(Vector3.zero, go_playerManager2.transform, "trainCard");
        }
    }
    private void Update()
    {
        ShuffleDeck(); // constatnly check if the deck needs to be shuffled (when the trainCards.Count is 0 or less)
    }

    private void AddingCards(int trainType, GameObject cardType) // adding cards into the Deck
    {
        for (int i = 0; i < trainType; i++) // loops for how many cards are needed for the trainType
        {
            trainCards.Add(cardType); // adds the specific trainCard into the trainCards list
        }
    }

    public GameObject DrawCard(Vector3 position, Transform parent, string tag) // whenever a card is drawn into, players hand or market ...
    {
        bool p1_wasDeactive = false;
        bool p2_wasDeactive = false;

        if (!go_playerManager1.activeSelf)
        {
            p1_wasDeactive = true;
            go_playerManager1.SetActive(true);
        }
        if (!go_playerManager2.activeSelf)
        {
            p2_wasDeactive = true;
            go_playerManager2.SetActive(true);
        }
        int randomNumber = Random.Range(0, trainCards.Count - 1); // find a random number within the trainCards.Count (-1 to match the size)
        Instantiate(trainCards[randomNumber], position, Quaternion.identity, parent); // spawn a trainCard from the list with the randomNumber as the index, spawn it at the given postion and parent
        trainCards.RemoveAt(randomNumber); // remove that trainCard so that it cannot be repeated again.
        trainCards[randomNumber].tag = tag; // tag the traimCard with the given tag
        trainCards[randomNumber].transform.SetAsFirstSibling();
        if (p1_wasDeactive)
        {
            go_playerManager1.SetActive(false);
            p1_wasDeactive = false;
            p2_wasDeactive = false;
        }
        else if (p2_wasDeactive)
        {
            go_playerManager2.SetActive(false);
            p1_wasDeactive = false;
            p2_wasDeactive = false;
        }
        return trainCards[randomNumber];

    }

    public void DiscardCard(GameObject card) //to keep the cards that are discarded and to be used again.
    {
        discardedTrainCards.Add(card); // add the card to the list
        card.transform.SetParent(discardedPile.transform); // set the cards parent to the discard pile
        card.SetActive(false); // set unactive
        Debug.Log("Card discarded");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(canDrawTrainCard == true)
        {
            if (cs_playerManager1.playerTurn)
            {
                DrawCard(Vector3.zero, go_playerManager1.transform, "trainCard");
            }
            else if (cs_playerManager2.playerTurn)
            {
                DrawCard(Vector3.zero, go_playerManager2.transform, "trainCard");
            }

            cardPickUpCounter++;
        }
    }

    public void ShuffleDeck()
    {
        if (trainCards.Count <= 0) //if the trainCards list is empty
        {
            for (int i = 0; i < discardedTrainCards.Count; i++) // loop though the discrdedTrainCards 
            {
                discardedTrainCards[i].SetActive(true); // set the cards to active
                trainCards.Add(discardedTrainCards[i]); // add the card to the trainCards list
                Debug.Log("cards reshuffled" + i);

                if (trainCards.Count == discardedTrainCards.Count) // once the discardedCards and trainCards lists are the same Count (all discarded cards added into trainCards)
                {
                    discardedTrainCards.Clear(); // Clear the discarded pile
                }
            }
            Debug.Log("deck reshuffled");
        }
    }

}
