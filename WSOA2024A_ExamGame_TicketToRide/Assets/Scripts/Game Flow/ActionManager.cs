using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    #region VARIABLES:
    [Header("Is The Turn Over")]
    [SerializeField] private bool isTurnOver;

    [Header("Amount of cards picked up")]
    [SerializeField] public int pickUpLocoCount;
    [SerializeField] public int pickUpNormCount;

    [Header("Can draw loco and norm cards")]
    [SerializeField] public bool canDrawLoco;
    [SerializeField] public bool canDrawNorm;

    [Header("Route action stuff")]
    [SerializeField] public bool canClaimRoute;
    [SerializeField] public int amountRoutesClaimed;

    [Header("Buttons to look after")]
    [SerializeField] public Button endPlayer1TurnButton;
    [SerializeField] public Button endPlayer2TurnButton;
    #endregion

    #region OTHER SCRIPTS:
    [SerializeField] DestinationDeck cs_destinationDeck;
    [SerializeField] TrainDeckManager cs_traindeckmanager;
    [SerializeField] GameManager cs_gameManager;
    #endregion

    private void Start()
    {
        canDrawLoco = true;
        canDrawNorm = true;
        canClaimRoute = true;
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
            cs_destinationDeck.canDrawDestinationCard = false;
            cs_traindeckmanager.canDrawTrainCard = false;
            canDrawLoco = false;
            canDrawNorm = false;
            canClaimRoute = false;
        }
        if(isTurnOver ==false)
        {
            cs_destinationDeck.canDrawDestinationCard = true;
            cs_traindeckmanager.canDrawTrainCard = true;
        }
    }

    public void CheckAction()
    {
        //Gets an int value from the sum of all the norm cards collected from the train market and the total cards colected from the train deck
        int maxCardsCollected = pickUpNormCount + cs_traindeckmanager.cardPickUpCounter;

        if (cs_destinationDeck.destinationCardAction == true)
        {
            isTurnOver = true;
        }
        //checks if 3 cards have been taken from the train deck
        if (maxCardsCollected == 2)
        {
            isTurnOver = true;
        }
        //checks if a card from the train deck has been picked up
        if(cs_traindeckmanager.cardPickUpCounter >= 1)
        {
            cs_destinationDeck.canDrawDestinationCard = false;
            canClaimRoute = false;
        }
        //checks if a locomotive card has been taken from the train market
        if(pickUpLocoCount >= 1)
        {
            isTurnOver = true;
            canDrawNorm = false;
        }
        if(pickUpNormCount >= 1)
        {
            canDrawLoco = false;
        }
        //Check if 1 normal card has been taken from the train market
        if(maxCardsCollected >= 1)
        {
            canDrawLoco = false;
            cs_destinationDeck.canDrawDestinationCard = false;
            canClaimRoute = false;
        }
        if(amountRoutesClaimed == 1)
        {
            isTurnOver = true;
        }

        //Button manager stuff
        if(isTurnOver == true)
        {
            endPlayer1TurnButton.interactable = true;
            endPlayer2TurnButton.interactable = true;
        }
        else if (isTurnOver == false)
        {
            endPlayer1TurnButton.interactable = false;
            endPlayer2TurnButton.interactable = false;
        }
    }

    //turns next turn button off
    public void PlayNextTurn()
    {
        cs_destinationDeck.destinationCardAction = false;
        cs_traindeckmanager.canDrawTrainCard = false;
        isTurnOver = false;
        canDrawNorm = true;
        canDrawLoco = true;
        canClaimRoute = true;

        cs_traindeckmanager.cardPickUpCounter = 0;
        pickUpLocoCount = 0;
        pickUpNormCount = 0;
        amountRoutesClaimed = 0;

        cs_gameManager.currentTurn++;
    }

    public void ChangeStateP1()
    {
        GameManager.Instance.UpdateGameState(GameState.Player1Turn);
    }

    public void ChangeStateP2()
    {
        GameManager.Instance.UpdateGameState(GameState.Player2Turn);
    }
}
