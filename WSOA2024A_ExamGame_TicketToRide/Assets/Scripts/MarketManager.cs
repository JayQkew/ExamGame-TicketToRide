using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    #region CARD SLOTS:
    [SerializeField] public GameObject cardSlot1;
    [SerializeField] public GameObject cardSlot2;
    [SerializeField] public GameObject cardSlot3;
    [SerializeField] public GameObject cardSlot4;
    [SerializeField] public GameObject cardSlot5;
    #endregion

    #region OTHER SCRIPTS:
    [SerializeField] TrainDeckManager cs_trainDeckManager;
    [SerializeField] GameObject trainDeck;
    [SerializeField] TrainCard cs_trainCard;
    [SerializeField] GameObject trainCard;
    #endregion

    [SerializeField] private int maxLocomotives = 3;
    [SerializeField] public int locomotivesOnMarket = 0;
    private void Start()
    {
        cs_trainDeckManager = trainDeck.GetComponent<TrainDeckManager>();
        cs_trainCard = trainCard.GetComponent<TrainCard>();
        Mathf.Clamp(locomotivesOnMarket, 0, 3);
        FillSlots();
    }
    public void FillSlots()
    {
        // Debug.Log("function start");
        cs_trainDeckManager.DrawCard(cardSlot1.transform.position, cardSlot1.transform);
        cs_trainDeckManager.DrawCard(cardSlot2.transform.position, cardSlot2.transform);
        cs_trainDeckManager.DrawCard(cardSlot3.transform.position, cardSlot3.transform);
        cs_trainDeckManager.DrawCard(cardSlot4.transform.position, cardSlot4.transform);
        cs_trainDeckManager.DrawCard(cardSlot5.transform.position, cardSlot5.transform);
        // Debug.Log("function end");
    }

    public void ResetMarket()
    {
        if (locomotivesOnMarket == maxLocomotives)
        {

        }
    }

}
