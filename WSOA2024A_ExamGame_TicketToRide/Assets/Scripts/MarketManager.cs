using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    #region CARD SLOTS:
    [SerializeField] GameObject cardSlot1;
    [SerializeField] GameObject cardSlot2;
    [SerializeField] GameObject cardSlot3;
    [SerializeField] GameObject cardSlot4;
    [SerializeField] GameObject cardSlot5;
    #endregion

    #region OTHER SCRIPTS:
    [SerializeField] TrainDeckManager cs_trainDeckManager;
    [SerializeField] GameObject trainDeck;
    [SerializeField] TrainCard cs_trainCard;
    [SerializeField] GameObject trainCard;
    [SerializeField] bool slotEmpty = true;
    #endregion

    private void Start()
    {
        cs_trainDeckManager = trainDeck.GetComponent<TrainDeckManager>();
        cs_trainCard = trainCard.GetComponent<TrainCard>();
        FillSlots();
    }
    public void FillSlots()
    {
        // Debug.Log("function start");
        cs_trainDeckManager.DrawCard(cardSlot1.transform.position);
        cs_trainDeckManager.DrawCard(cardSlot2.transform.position);
        cs_trainDeckManager.DrawCard(cardSlot3.transform.position);
        cs_trainDeckManager.DrawCard(cardSlot4.transform.position);
        cs_trainDeckManager.DrawCard(cardSlot5.transform.position);
        // Debug.Log("function end");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "trainCard")
        {
            slotEmpty = true;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "trainCard")
        {
            slotEmpty = false;
        }
    }
}
