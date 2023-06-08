using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionManager : MonoBehaviour
{
    //variables
    [SerializeField] public GameObject nextTurnButton;
    [SerializeField] private bool isTurnOver;
    [SerializeField] public int cardPickUpCounter;

    //other scrips
    [SerializeField] DestinationDeck cs_destinationDeck;
    [SerializeField] TrainDeckManager cs_traindeckmanager;

    private void Awake()
    {
        nextTurnButton.SetActive(false);
    }

    private void Update()
    {
        TakeNextTurn();
        CheckAction();
    }

    //checks if a turn was taken or not
    public void TakeNextTurn()
    {
        if(isTurnOver == true)
        {
            nextTurnButton.SetActive(true);
            cs_destinationDeck.canDrawDestinationCard = false;
        }
        if(isTurnOver ==false)
        {
            nextTurnButton.SetActive(false);
            cs_destinationDeck.canDrawDestinationCard = true;
        }
    }

    public void CheckAction()
    {
        if (cs_destinationDeck.destinationCardAction == true)
        {
            isTurnOver = true;
        }
    }

    //turns next turn button off
    public void PlayNextTurn()
    {
        cs_destinationDeck.destinationCardAction = false;
        isTurnOver = false;
    }
}
