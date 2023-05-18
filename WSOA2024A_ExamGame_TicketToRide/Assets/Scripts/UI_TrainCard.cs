using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_TrainCard : MonoBehaviour, IPointerClickHandler
{
    #region OTHER SCRIPTS:
    [SerializeField] public SO_TrainCards so_trainCards;
    [SerializeField] UI_PlayerManager cs_playerManager;
    [SerializeField] UI_MarketManager cs_marketManager;
    [SerializeField] UI_TrainDeckManager cs_trainDeckManager;
    #endregion

    void Awake()
    {
        #region GETTING OTHER SCRIPTS:
        cs_playerManager = GameObject.Find("Player_1").GetComponent<UI_PlayerManager>();

        cs_marketManager = GameObject.Find("TrainMarket").GetComponent<UI_MarketManager>();

        cs_trainDeckManager = GameObject.Find("TrainDeck").GetComponent<UI_TrainDeckManager>();
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
        if (so_trainCards.isLocomotive == true)       // checking if its a locomotive
        {
            switch (transform.parent.name)  // checks if the parents name is "cardSlot_". If it is, replace it with another card.
            {
                case "cardSlot1":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[0].transform.position, cs_marketManager.cardSlots[0].transform, "marketTrainCard");
                    cs_playerManager.trainHandCards.Add(eventData.pointerClick);
                    cs_playerManager.SortTrainHand();
                    cs_marketManager.locomotivesOnMarket--;  // if it is, decreace the locomotives count on the market
                    break;
                case "cardSlot2":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[1].transform.position, cs_marketManager.cardSlots[1].transform, "marketTrainCard");
                    cs_playerManager.trainHandCards.Add(eventData.pointerClick);
                    cs_playerManager.SortTrainHand();
                    cs_marketManager.locomotivesOnMarket--;  // if it is, decreace the locomotives count on the market
                    break;
                case "cardSlot3":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[2].transform.position, cs_marketManager.cardSlots[2].transform, "marketTrainCard");
                    cs_playerManager.trainHandCards.Add(eventData.pointerClick);
                    cs_playerManager.SortTrainHand();
                    cs_marketManager.locomotivesOnMarket--;  // if it is, decreace the locomotives count on the market
                    break;
                case "cardSlot4":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[3].transform.position, cs_marketManager.cardSlots[3].transform, "marketTrainCard");
                    cs_playerManager.trainHandCards.Add(eventData.pointerClick);
                    cs_playerManager.SortTrainHand();
                    cs_marketManager.locomotivesOnMarket--;  // if it is, decreace the locomotives count on the market
                    break;
                case "cardSlot5":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[4].transform.position, cs_marketManager.cardSlots[4].transform, "marketTrainCard");
                    cs_playerManager.trainHandCards.Add(eventData.pointerClick);
                    cs_playerManager.SortTrainHand();
                    cs_marketManager.locomotivesOnMarket--;  // if it is, decreace the locomotives count on the market
                    break;
                case "trainHand": // if its already in the train hand, pop it to the front of the trainhand
                    cs_playerManager.trainHandCards.Remove(eventData.pointerClick.gameObject);
                    cs_trainDeckManager.DiscardCard(eventData.pointerClick.gameObject);
                    break;

                default:
                    Debug.Log("you fucked up");
                    break;
            }
        }
        else if (so_trainCards.isLocomotive == false)
        {
            switch (transform.parent.name)  // checks the parents name. If it is cardSlot_, replace it with another card.
            {
                case "cardSlot1":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[0].transform.position, cs_marketManager.cardSlots[0].transform, "marketTrainCard");
                    cs_playerManager.trainHandCards.Add(eventData.pointerClick);
                    cs_playerManager.SortTrainHand();
                    cs_marketManager.ResetMarket(); // check if it needs to reset the market
                    break;
                case "cardSlot2":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[1].transform.position, cs_marketManager.cardSlots[1].transform, "marketTrainCard");
                    cs_playerManager.trainHandCards.Add(eventData.pointerClick);
                    cs_playerManager.SortTrainHand();
                    cs_marketManager.ResetMarket();
                    break;
                case "cardSlot3":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[2].transform.position, cs_marketManager.cardSlots[2].transform, "marketTrainCard");
                    cs_playerManager.trainHandCards.Add(eventData.pointerClick);
                    cs_playerManager.SortTrainHand();
                    cs_marketManager.ResetMarket();
                    break;
                case "cardSlot4":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[3].transform.position, cs_marketManager.cardSlots[3].transform, "marketTrainCard");
                    cs_playerManager.trainHandCards.Add(eventData.pointerClick);
                    cs_playerManager.SortTrainHand();
                    cs_marketManager.ResetMarket();
                    break;
                case "cardSlot5":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[4].transform.position, cs_marketManager.cardSlots[4].transform, "marketTrainCard");
                    cs_playerManager.trainHandCards.Add(eventData.pointerClick);
                    cs_playerManager.SortTrainHand();
                    cs_marketManager.ResetMarket();
                    break;
                case "trainHand": // if its already in the train hand, pop it to the front of the trainhand
                    cs_playerManager.trainHandCards.Remove(eventData.pointerClick.gameObject);
                    cs_trainDeckManager.DiscardCard(eventData.pointerClick.gameObject);
                    Debug.Log(cs_playerManager.trainHandCards.IndexOf(gameObject));
                    for (int i = 0; i < cs_playerManager.trainHandCards.IndexOf(gameObject); i++)
                    {
                        cs_playerManager.trainHandCards[i].gameObject.transform.position += new Vector3(20, 0, 0);
                    }
                        break;
                default:
                    Debug.Log("you fucked up");
                    break;

            }
        }
        gameObject.tag = "trainCard"; // assign tag "trainCard" to the gameObject
        if (transform.parent.name == "trainHand") 
        {
            transform.SetParent(cs_trainDeckManager.discardedPile.transform);
        }
        else
        {
            transform.SetParent(cs_playerManager.trainHand);
        }
        transform.position = cs_playerManager.trainHand.position;

        
    }

}
