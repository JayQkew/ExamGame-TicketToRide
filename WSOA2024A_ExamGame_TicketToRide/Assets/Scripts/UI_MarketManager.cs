using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MarketManager : MonoBehaviour
{
    #region OTHER SCRIPTS:
    [SerializeField] UI_TrainDeckManager cs_trainDeckManager;
    [SerializeField] GameObject trainDeck;
    [SerializeField] UI_TrainCard cs_trainCard;
    [SerializeField] GameObject trainCard;
    #endregion

    [SerializeField] public GameObject[] cardSlots = new GameObject[5];
    [SerializeField] GameObject[] trainCards;
    [SerializeField] public int locomotivesOnMarket = 0;

    private void Awake()
    {
        #region GETTING OTHER SCRIPTS:
        cs_trainDeckManager = trainDeck.GetComponent<UI_TrainDeckManager>();
        cs_trainCard = trainCard.GetComponent<UI_TrainCard>();
        #endregion
    }
    private void Start()
    {
        FillSlots();
    }
    private void Update()
    {
        ResetMarket();
    }
    public void FillSlots()
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            cs_trainDeckManager.DrawCard(cardSlots[i].transform.position, cardSlots[i].transform, "marketTrainCard");
            // Debug.Log("tag assigned");
        }
    }

    public void ResetMarket()
    {
        if (locomotivesOnMarket >= 3)
        {
            trainCards = GameObject.FindGameObjectsWithTag("marketTrainCard");
            foreach (GameObject go in trainCards)
            {
                cs_trainDeckManager.DiscardCard(go);
            }
            locomotivesOnMarket = 0;
            FillSlots();
        }
    }

}
