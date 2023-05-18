using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerManager : MonoBehaviour
{
    #region VARIABLES:
    [SerializeField] public List<GameObject> trainHandCards = new List<GameObject>();
    [SerializeField] public List<GameObject> destinationHandCards = new List<GameObject>();
    [SerializeField] public Transform trainHand;
    [SerializeField] public Transform destinationHand;
    [SerializeField] public bool playerTurn;

    List<GameObject> redCards = new List<GameObject>();
    List<GameObject> greenCards = new List<GameObject>();
    List<GameObject> blueCards = new List<GameObject>();
    List<GameObject> blackCards = new List<GameObject>();
    List<GameObject> whiteCards = new List<GameObject>();
    List<GameObject> pinkCards = new List<GameObject>();
    List<GameObject> orangeCards = new List<GameObject>();
    List<GameObject> yellowCards = new List<GameObject>();
    List<GameObject> locomotiveCards = new List<GameObject>();
    #endregion

    #region OTHER SCRIPTS:
    [SerializeField] UI_TrainDeckManager cs_trainDeckManager;
    [SerializeField] GameObject go_trainDeckManager;
    [SerializeField] UI_TrainCard cs_trainCard;
    [SerializeField] GameObject go_trainCard;
    #endregion

    private void Awake()
    {
        cs_trainCard = go_trainCard.GetComponent<UI_TrainCard>();
        cs_trainDeckManager = go_trainDeckManager.GetComponent<UI_TrainDeckManager>();
    }

    public void SortTrainHand()
    {
        trainHandCards.Clear(); // clears the list in case there are any overlaps
        for (int i =0; i < trainHand.childCount; i++)
        {
            trainHandCards.Add(trainHand.GetChild(i).gameObject);
        }

        foreach (GameObject card in trainHandCards) // loops through all the cards
        {
            card.transform.SetParent(trainHand);
        }
    }

    public void SortDestinations()
    {
        destinationHandCards.Clear();
        for(int i = 0; i< destinationHandCards.Count; i++)
        {
        }
    }

    /*
    public void ArrangeHand()
    {
        foreach(GameObject card in trainHandCards)
        {
            switch (cs_trainCard.so_trainCards.colour) // add cards to their respective lists
            {
                case "red":
                    redCards.Add(card);
                    break;
                case "green":
                    greenCards.Add(card);
                    break;
                case "blue":
                    blueCards.Add(card);
                    break;
                case "black":
                    blackCards.Add(card);
                    break;
                case "white":
                    whiteCards.Add(card);
                    break;
                case "pink":
                    pinkCards.Add(card);
                    break;
                case "orange":
                    orangeCards.Add(card);
                    break;
                case "yellow":
                    yellowCards.Add(card);
                    break;
                case "loco":
                    locomotiveCards.Add(card);
                    break;
                    default: break;
            }
        }
        trainHandCards.Clear(); // clear the hand

        int initialChildCount = trainHand.childCount;

        for (int i = 0; i < initialChildCount; i++)
        {
            trainHand.GetChild(i).gameObject.SetActive(false);
        }

        ArrangeHandLoop(redCards); // re-add the cards in order
        ArrangeHandLoop(greenCards);
        ArrangeHandLoop(blueCards);
        ArrangeHandLoop(blackCards);
        ArrangeHandLoop(whiteCards);
        ArrangeHandLoop(pinkCards);
        ArrangeHandLoop(orangeCards);
        ArrangeHandLoop(yellowCards);
        ArrangeHandLoop(locomotiveCards);
    }

    private void ArrangeHandLoop(List<GameObject> cards)
    {
        foreach (GameObject card in cards)
        {
            trainHandCards.Add(card);
            Instantiate(card, trainHand);
        }

        cards.Clear();
    }
    */
}
