using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_TrainCard : MonoBehaviour, IPointerClickHandler
{
    #region OTHER SCRIPTS:
    [SerializeField] SO_TrainCards so_trainCards;
    [SerializeField] UI_PlayerManager cs_playerHand;
    [SerializeField] GameObject go_playerhand;
    [SerializeField] UI_MarketManager cs_marketManager;
    [SerializeField] GameObject go_marketManager;
    [SerializeField] UI_TrainDeckManager cs_trainDeckManager;
    [SerializeField] GameObject go_trainDeckManager;
    #endregion

    void Awake()
    {
        #region GETTING OTHER SCRIPTS:
        go_playerhand = GameObject.Find("Player_1");
        cs_playerHand = go_playerhand.GetComponent<UI_PlayerManager>();

        go_marketManager = GameObject.Find("TrainMarket");
        cs_marketManager = go_marketManager.GetComponent<UI_MarketManager>();

        go_trainDeckManager = GameObject.Find("TrainDeck");
        cs_trainDeckManager = go_trainDeckManager.GetComponent<UI_TrainDeckManager>();
        #endregion
    }

    void Start()
    {
        if (so_trainCards.isLocomotive == true)
        {
            switch (transform.parent.name)  // checks if the parents name is "cardSlot_", if it is... add 1 to the locomotivesOnMarket.
            {
                case "cardSlot1":
                case "cardSlot2":
                case "cardSlot3":
                case "cardSlot4":
                case "cardSlot5":
                    cs_marketManager.locomotivesOnMarket++;
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
        Debug.Log("even captured");
            if (so_trainCards.isLocomotive == true)       // checking if its a locomotive
            {
                cs_marketManager.locomotivesOnMarket--;  // if it is, decreace the locomotives count on the market
                // Debug.Log("locomotivesOnMarket - 1");
                // Debug.Log("locomotivesOnMarket =" + cs_marketManager.locomotivesOnMarket);
            }


            switch (transform.parent.name)  // checks if the parents name is "cardSlot_". If it is, replace it with another card.
            {
                case "cardSlot1":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[0].transform.position, cs_marketManager.cardSlots[0].transform, "marketTrainCard");
                    // Debug.Log("card replaced");
                    cs_marketManager.ResetMarket();
                    break;
                case "cardSlot2":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[1].transform.position, cs_marketManager.cardSlots[1].transform, "marketTrainCard");
                    cs_marketManager.ResetMarket();
                    break;
                case "cardSlot3":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[2].transform.position, cs_marketManager.cardSlots[2].transform, "marketTrainCard");
                    cs_marketManager.ResetMarket();
                    break;
                case "cardSlot4":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[3].transform.position, cs_marketManager.cardSlots[3].transform, "marketTrainCard");
                    cs_marketManager.ResetMarket();
                    break;
                case "cardSlot5":
                    cs_trainDeckManager.DrawCard(cs_marketManager.cardSlots[4].transform.position, cs_marketManager.cardSlots[4].transform, "marketTrainCard");
                    cs_marketManager.ResetMarket();
                    break;
                default:
                    Debug.Log("you fucked up");
                    break;

            }
        gameObject.tag = "trainCard";
        transform.SetParent(cs_playerHand.handSlot.transform, true);    // object is parented to handSlot
        transform.position = cs_playerHand.handSlot.transform.position;     // change position to handSlot
            // Debug.Log("card added");

        
    }

}
