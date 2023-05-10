using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TrainDeckManager : MonoBehaviour
{
    #region CARD PILE LISTS:
    [SerializeField] List<GameObject> trainCards = new List<GameObject>();
    [SerializeField] List<GameObject> discardedTrainCards = new List<GameObject>();
    #endregion

    #region TRAIN INT VARIABLES:
    private int redTrains = 12;
    private int greenTrains = 12;
    private int blueTrains = 12;
    private int blackTrains = 12;
    private int whiteTrains = 12;
    private int pinkTrains = 12;
    private int orangeTrains = 12;
    private int yellowTrains = 12;
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

    [SerializeField] GameObject trainDeck;
    [SerializeField] GameObject trainHand;
    [SerializeField] GameObject discardedPile;
    #endregion

    #region OTHER SCRIPTS:
    [SerializeField] UI_TrainCard cs_trainCard;
    [SerializeField] GameObject go_trainCard;
    [SerializeField] PlayerHand cs_playerHand;
    [SerializeField] GameObject go_playerHand;
    [SerializeField] MarketManager cs_marketManager;
    [SerializeField] GameObject go_marketManager;
    #endregion
    void Awake()
    {
        #region ADDING CARDS:
        AddingCards(redTrains, redCard);
        AddingCards(greenTrains, greenCard);
        AddingCards(blueTrains, blueCard);
        AddingCards(blackTrains, blackCard);
        AddingCards(whiteTrains, whiteCard);
        AddingCards(pinkTrains, pinkCard);
        AddingCards(orangeTrains, orangeCard);
        AddingCards(yellowTrains, yellowCard);
        AddingCards(locomotive, locomotiveCard);
        #endregion

        #region GETTING OTHER SCRIPTS:
        cs_playerHand = go_playerHand.GetComponent<PlayerHand>();
        cs_trainCard = go_trainCard.GetComponent<UI_TrainCard>();
        cs_marketManager = go_marketManager.GetComponent<MarketManager>();
        #endregion
    }
    private void Update()
    {
        ShuffleDeck();
    }

    private void AddingCards(int trainType, GameObject cardType)
    {
        for (int i = 0; i < trainType; i++)
        {
            trainCards.Add(cardType); // adds the trainCard into the list
        }
    }

    public void DrawCard(Vector3 position, Transform parent, string tag)
    {
        int randomNumber = Random.Range(0, trainCards.Count - 1);
        Instantiate(trainCards[randomNumber], position, Quaternion.identity, parent);
        trainCards.RemoveAt(randomNumber);
        trainCards[randomNumber].gameObject.tag = tag;
    }

    public void DiscardCard(GameObject card)
    {
        discardedTrainCards.Add(card);
        card.transform.parent = discardedPile.transform;
        card.SetActive(false);
        Debug.Log("card destroyed");
    }

    public void OnPressDeck()
    {
        DrawCard(cs_playerHand.handSlot.transform.position, cs_playerHand.handSlot.transform, "trainCard"); // players hand
    }

    public void ShuffleDeck()
    {
        if (trainCards.Count <= 0)
        {
            for (int i = 0; i < discardedTrainCards.Count; i++)
            {
                discardedTrainCards[i].SetActive(true);
                trainCards.Add(discardedTrainCards[i]);
                Debug.Log("cards reshuffled" + i);

                if (trainCards.Count == discardedTrainCards.Count)
                {
                    Debug.Log("Condition met");
                    for (int j = i; j >= 0; j--)
                    {
                        discardedTrainCards.Remove(discardedTrainCards[j]);
                        Debug.Log("cards removed from discarded pile: " + j);
                    }
                }
            }
            Debug.Log("deck reshuffled");
        }
    }

}
