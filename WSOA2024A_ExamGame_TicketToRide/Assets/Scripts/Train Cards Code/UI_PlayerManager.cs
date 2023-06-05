using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerManager : MonoBehaviour
{
    #region VARIABLES:
    [SerializeField] public List<GameObject> trainHandCards = new List<GameObject>();
    [SerializeField] public List<GameObject> destinationHandCards = new List<GameObject>();
    [SerializeField] public Transform[] colourPiles = new Transform[9];
    [SerializeField] public Transform destinationHand;
    [SerializeField] public bool playerTurn;

    #endregion

    #region OTHER SCRIPTS:
    [SerializeField] UI_TrainDeckManager cs_trainDeckManager;
    [SerializeField] GameObject go_trainDeckManager;
    [SerializeField] UI_TrainCard cs_trainCard;
    [SerializeField] GameObject go_trainCard;
    #endregion

    private void Awake()
    {
        cs_trainCard = go_trainCard.GetComponent<UI_TrainCard>();
        cs_trainDeckManager = go_trainDeckManager.GetComponent<UI_TrainDeckManager>();
    }

    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {

            if (transform.GetChild(i).tag == "trainCard" || transform.GetChild(i).tag == "marketTrainCard")
            {
                GameObject card = transform.GetChild(i).gameObject;
                string colour = card.GetComponent<UI_TrainCard>().so_trainCards.colour;

                switch (colour)
                {
                    case "black":
                        card.transform.SetParent(colourPiles[0]);
                        card.transform.SetAsFirstSibling();
                        card.transform.position = colourPiles[0].position;
                        break;
                    case "blue":
                        card.transform.SetParent(colourPiles[1]);
                        card.transform.SetAsFirstSibling();
                        card.transform.position = colourPiles[1].position;
                        break;
                    case "green":
                        card.transform.SetParent(colourPiles[2]);
                        card.transform.SetAsFirstSibling();
                        card.transform.position = colourPiles[2].position;
                        break;
                    case "pink":
                        card.transform.SetParent(colourPiles[3]);
                        card.transform.SetAsFirstSibling();
                        card.transform.position = colourPiles[3].position;
                        break;
                    case "red":
                        card.transform.SetParent(colourPiles[4]);
                        card.transform.SetAsFirstSibling();
                        card.transform.position = colourPiles[4].position;
                        break;
                    case "white":
                        card.transform.SetParent(colourPiles[5]);
                        card.transform.SetAsFirstSibling();
                        card.transform.position = colourPiles[5].position;
                        break;
                    case "orange":
                        card.transform.SetParent(colourPiles[6]);
                        card.transform.SetAsFirstSibling();
                        card.transform.position = colourPiles[6].position;
                        break;
                    case "yellow":
                        card.transform.SetParent(colourPiles[7]);
                        card.transform.SetAsFirstSibling();
                        card.transform.position = colourPiles[7].position;
                        break;
                    case "loco":
                        card.transform.SetParent(colourPiles[8]);
                        card.transform.SetAsFirstSibling();
                        card.transform.position = colourPiles[8].position;
                        break;

                }
            }
        }
    }

}
