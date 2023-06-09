using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrainCard : MonoBehaviour, IPointerClickHandler
{
    #region OTHER SCRIPTS:
    [SerializeField] public SO_TrainCards so_trainCards;
    [SerializeField] PlayerManager cs_playerManager1;
    [SerializeField] PlayerManager cs_playerManager2;
    [SerializeField] MarketManager cs_marketManager;
    [SerializeField] TrainDeckManager cs_trainDeckManager;
    [SerializeField] ActionManager cs_actionManager;
    #endregion

    #region OTHER VARIABLES:
    #endregion

    void Awake()
    {
        #region GETTING OTHER SCRIPTS:
        cs_marketManager = GameObject.Find("TrainMarket").GetComponent<MarketManager>();

        cs_trainDeckManager = GameObject.Find("TrainDeck").GetComponent<TrainDeckManager>();

        cs_playerManager1 = GameObject.Find("Player_1").GetComponent<PlayerManager>();

        cs_playerManager2 = GameObject.Find("Player_2").GetComponent<PlayerManager>();

        cs_actionManager = GameObject.Find("Players").GetComponent<ActionManager>();
        #endregion
    }

    void Start()
    {
        if (so_trainCards.isLocomotive == true) // checks if the card is a locomotive at the start of the game, if true, run the code
        {
            switch (transform.parent.name)  // if it is the child of any cardSlot
            {
                case "cardSlot1":
                case "cardSlot2":
                case "cardSlot3":
                case "cardSlot4":
                case "cardSlot5":
                    cs_marketManager.locomotivesOnMarket++; // increase the locomotiveOnMarket
                    Debug.Log("locomotiveOnMarket = " + cs_marketManager.locomotivesOnMarket);
                    break;
                default:
                    Debug.Log("you fucked up");
                    break;
            }

        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(cs_actionManager.canDrawLoco == true) //checks if a locomotive card can be picked up
        {
            if(cs_actionManager.pickUpNormCount < 1) //checks if a normal card has not been picked up
            {
                if (cs_playerManager1.playerTurn)
                {
                    PlayerCheck(cs_playerManager1, eventData);
                }
                else if (cs_playerManager2.playerTurn)
                {
                    PlayerCheck(cs_playerManager2, eventData);
                }
            }
        }
        if(cs_actionManager.canDrawNorm == true) //checks if a normal card can be picked up
        {
            if(cs_actionManager.pickUpLocoCount < 1) //checks if a locomotive card has not been
            {
                if (cs_playerManager1.playerTurn)
                {
                    PlayerCheck(cs_playerManager1, eventData);
                }
                else if (cs_playerManager2.playerTurn)
                {
                    PlayerCheck(cs_playerManager2, eventData);
                }
            }
        }
    }

    private void PlayerCheck(PlayerManager cs_playerManagerScript, PointerEventData eventData)
    {
        if (so_trainCards.isLocomotive)       // checking if its a locomotive
        {
            switch (transform.parent.name)  // checks if the parents name is "cardSlot_". If it is, replace it with another card.
            {
                case "cardSlot1":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[0].transform.position, cs_marketManager.cardSlots[0].transform, "marketTrainCard");
                    cs_playerManagerScript.trainHandCards.Add(eventData.pointerClick);
                    cs_marketManager.locomotivesOnMarket--;  // if it is, decreace the locomotives count on the market
                    break;
                case "cardSlot2":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[1].transform.position, cs_marketManager.cardSlots[1].transform, "marketTrainCard");
                    cs_playerManagerScript.trainHandCards.Add(eventData.pointerClick);
                    cs_marketManager.locomotivesOnMarket--;  // if it is, decreace the locomotives count on the market
                    break;
                case "cardSlot3":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[2].transform.position, cs_marketManager.cardSlots[2].transform, "marketTrainCard");
                    cs_playerManagerScript.trainHandCards.Add(eventData.pointerClick);
                    cs_marketManager.locomotivesOnMarket--;  // if it is, decreace the locomotives count on the market
                    break;
                case "cardSlot4":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[3].transform.position, cs_marketManager.cardSlots[3].transform, "marketTrainCard");
                    cs_playerManagerScript.trainHandCards.Add(eventData.pointerClick);
                    cs_marketManager.locomotivesOnMarket--;  // if it is, decreace the locomotives count on the market
                    break;
                case "cardSlot5":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[4].transform.position, cs_marketManager.cardSlots[4].transform, "marketTrainCard");
                    cs_playerManagerScript.trainHandCards.Add(eventData.pointerClick);
                    cs_marketManager.locomotivesOnMarket--;  // if it is, decreace the locomotives count on the market
                    break;
                case "trainHand": // if its already in the train hand, pop it to the front of the trainhand
                    cs_playerManagerScript.trainHandCards.Remove(eventData.pointerClick.gameObject);
                    cs_trainDeckManager.DiscardCard(eventData.pointerClick.gameObject);
                    break;

                default:
                    Debug.Log("you fucked up");
                    break;
            }
            cs_actionManager.pickUpLocoCount += 1;
        }

        if (!so_trainCards.isLocomotive)
        {
            switch (transform.parent.name)  // checks the parents name. If it is cardSlot_, replace it with another card.
            {
                case "cardSlot1":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[0].transform.position, cs_marketManager.cardSlots[0].transform, "marketTrainCard");
                    cs_playerManagerScript.trainHandCards.Add(eventData.pointerClick);
                    cs_marketManager.ResetMarket(); // check if it needs to reset the market
                    break;
                case "cardSlot2":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[1].transform.position, cs_marketManager.cardSlots[1].transform, "marketTrainCard");
                    cs_playerManagerScript.trainHandCards.Add(eventData.pointerClick);
                    cs_marketManager.ResetMarket();
                    break;
                case "cardSlot3":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[2].transform.position, cs_marketManager.cardSlots[2].transform, "marketTrainCard");
                    cs_playerManagerScript.trainHandCards.Add(eventData.pointerClick);
                    cs_marketManager.ResetMarket();
                    break;
                case "cardSlot4":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[3].transform.position, cs_marketManager.cardSlots[3].transform, "marketTrainCard");
                    cs_playerManagerScript.trainHandCards.Add(eventData.pointerClick);
                    cs_marketManager.ResetMarket();
                    break;
                case "cardSlot5":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[4].transform.position, cs_marketManager.cardSlots[4].transform, "marketTrainCard");
                    cs_playerManagerScript.trainHandCards.Add(eventData.pointerClick);
                    cs_marketManager.ResetMarket();
                    break;
                case "trainHand": // if its already in the train hand, pop it to the front of the trainhand
                    cs_playerManagerScript.trainHandCards.Remove(eventData.pointerClick.gameObject);
                    cs_trainDeckManager.DiscardCard(eventData.pointerClick.gameObject);
                    Debug.Log(cs_playerManagerScript.trainHandCards.IndexOf(gameObject));
                    for (int i = 0; i < cs_playerManagerScript.trainHandCards.IndexOf(gameObject); i++)
                    {
                        cs_playerManagerScript.trainHandCards[i].gameObject.transform.position += new Vector3(20, 0, 0);
                    }
                    break;
                default:
                    Debug.Log("you fucked up");
                    break;

            }
            cs_actionManager.pickUpNormCount += 1;
        }

        gameObject.tag = "trainCard"; // assign tag "trainCard" to the gameObject
        if (transform.parent.name == "trainHand")
        {
            transform.SetParent(cs_trainDeckManager.discardedPile.transform);
        }
        else
        {
            switch (so_trainCards.colour)
            {
                case "black":
                    transform.SetParent(cs_playerManagerScript.colourPiles[0].transform);
                    transform.SetAsFirstSibling();
                    transform.position = cs_playerManagerScript.colourPiles[0].transform.position;
                    break;
                case "blue":
                    transform.SetParent(cs_playerManagerScript.colourPiles[1].transform);
                    transform.SetAsFirstSibling();
                    transform.position = cs_playerManagerScript.colourPiles[1].transform.position;
                    break;
                case "green":
                    transform.SetParent(cs_playerManagerScript.colourPiles[2].transform);
                    transform.SetAsFirstSibling();
                    transform.position = cs_playerManagerScript.colourPiles[2].transform.position;
                    break;
                case "pink":
                    transform.SetParent(cs_playerManagerScript.colourPiles[3].transform);
                    transform.SetAsFirstSibling();
                    transform.position = cs_playerManagerScript.colourPiles[3].transform.position;
                    break;
                case "red":
                    transform.SetParent(cs_playerManagerScript.colourPiles[4].transform);
                    transform.SetAsFirstSibling();
                    transform.position = cs_playerManagerScript.colourPiles[4].transform.position;
                    break;
                case "white":
                    transform.SetParent(cs_playerManagerScript.colourPiles[5].transform);
                    transform.SetAsFirstSibling();
                    transform.position = cs_playerManagerScript.colourPiles[5].transform.position;
                    break;
                case "orange":
                    transform.SetParent(cs_playerManagerScript.colourPiles[6].transform);
                    transform.SetAsFirstSibling();
                    transform.position = cs_playerManagerScript.colourPiles[6].transform.position;
                    break;
                case "yellow":
                    transform.SetParent(cs_playerManagerScript.colourPiles[7].transform);
                    transform.SetAsFirstSibling();
                    transform.position = cs_playerManagerScript.colourPiles[7].transform.position;
                    break;
                case "loco":
                    transform.SetParent(cs_playerManagerScript.colourPiles[8].transform);
                    transform.SetAsFirstSibling();
                    transform.position = cs_playerManagerScript.colourPiles[8].transform.position;
                    break;
            }
        }



    }
}
