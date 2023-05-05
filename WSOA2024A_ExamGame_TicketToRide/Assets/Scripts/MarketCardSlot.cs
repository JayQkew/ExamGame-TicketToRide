using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketCardSlot : MonoBehaviour
{
    [SerializeField] GameObject cardSlot;
    [SerializeField] public bool cardSlotAvailable = true;

    [SerializeField] TrainCard cs_trainCard;
    [SerializeField] GameObject go_trainCard;
    [SerializeField] MarketManager cs_marketManager;
    [SerializeField] GameObject go_marketManager;

    private void Start()
    {
        cs_trainCard = go_trainCard.GetComponent<TrainCard>();
        cs_marketManager = go_marketManager.GetComponent<MarketManager>();
    }
    private void Update()
    {
        if (cs_trainCard.transform.IsChildOf(cs_marketManager.cardSlot1.transform))             // try make a switch statement
        {
            cardSlotAvailable = false;
            Debug.Log("Here");
        }
        else if (cs_trainCard.transform.IsChildOf(cs_marketManager.cardSlot2.transform))
        {
            cardSlotAvailable = false;
            Debug.Log("Here");
        }
        else if (cs_trainCard.transform.IsChildOf(cs_marketManager.cardSlot3.transform))
        {
            cardSlotAvailable = false;
            Debug.Log("Here");
        }
        else if (cs_trainCard.transform.IsChildOf(cs_marketManager.cardSlot4.transform))
        {
            cardSlotAvailable = false;
            Debug.Log("Here");
        }
        else if (cs_trainCard.transform.IsChildOf(cs_marketManager.cardSlot5.transform))
        {
            cardSlotAvailable = false;
            Debug.Log("Here");
        }

        if (!cs_trainCard.transform.IsChildOf(cs_marketManager.cardSlot1.transform))             // try make a switch statement
        {
            cardSlotAvailable = true;
        }
        else if (!cs_trainCard.transform.IsChildOf(cs_marketManager.cardSlot2.transform))
        {
            cardSlotAvailable = true;
        }
        else if (!cs_trainCard.transform.IsChildOf(cs_marketManager.cardSlot3.transform))
        {
            cardSlotAvailable = true;
        }
        else if (!cs_trainCard.transform.IsChildOf(cs_marketManager.cardSlot4.transform))
        {
            cardSlotAvailable = true;
        }
        else if (!cs_trainCard.transform.IsChildOf(cs_marketManager.cardSlot5.transform))
        {
            cardSlotAvailable = true;
        }

    }
}
