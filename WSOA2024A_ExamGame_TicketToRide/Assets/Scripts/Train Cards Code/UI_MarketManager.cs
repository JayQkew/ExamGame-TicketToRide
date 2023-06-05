using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MarketManager : MonoBehaviour
{
    #region OTHER SCRIPTS:
    [SerializeField] UI_TrainDeckManager cs_trainDeckManager;
    [SerializeField] GameObject go_trainDeck;
    [SerializeField] UI_TrainCard cs_trainCard;
    [SerializeField] GameObject go_trainCard;
    #endregion

    [SerializeField] public GameObject[] cardSlots = new GameObject[5];
    [SerializeField] GameObject[] trainCards;
    [SerializeField] public int locomotivesOnMarket = 0;

    private void Awake()
    {
        #region GETTING OTHER SCRIPTS:
        cs_trainDeckManager = go_trainDeck.GetComponent<UI_TrainDeckManager>();
        cs_trainCard = go_trainCard.GetComponent<UI_TrainCard>();
        #endregion
    }
    private void Start()
    {
        FillSlots(); // Start the game by filling the slots
    }
    private void Update()
    {
        ResetMarket(); // constantly check if there are locomotive cards on the Market
    }
    public void FillSlots()
    {
        for (int i = 0; i < cardSlots.Length; i++) // loops for the how many cardSlots there are (5)
        {
            cs_trainDeckManager.DrawCard(cardSlots[i].transform.position, cardSlots[i].transform, "marketTrainCard"); // DrawCard(Vector3 position, Transform parent, string tag)
            // Debug.Log("tag assigned");
        }
    }

    public void ResetMarket() // if the locomotive limit has been reached in the market
    {
        if (locomotivesOnMarket >= 3) // locomotive limit reached
        {
            trainCards = GameObject.FindGameObjectsWithTag("marketTrainCard"); // finds gameObjects with the tag "marketTrainCard" and puts it into the trainCards array;
            foreach (GameObject go in trainCards) // for each trainCard in the array
            {
                cs_trainDeckManager.DiscardCard(go); // Discard the card ---- DiscardCard(GameObject card)
            }
            locomotivesOnMarket = 0; // set the locomotivesOnMarket to 0
            FillSlots(); // Fill the slots up 
        }
    }

}
