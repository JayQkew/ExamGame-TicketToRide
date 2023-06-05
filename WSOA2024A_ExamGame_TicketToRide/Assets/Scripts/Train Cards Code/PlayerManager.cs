using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region VARIABLES:
    [HideInInspector] public List<GameObject> trainHandCards = new List<GameObject>();
    [HideInInspector] public List<GameObject> destinationHandCards = new List<GameObject>();
    [HideInInspector] public GameObject[] colourPiles = new GameObject[9];
    public Transform destinationHand;
    [SerializeField] public bool playerTurn;
    [SerializeField] public int trainPieces = 40;
    [SerializeField] public int points = 0;
    #endregion

    #region OTHER SCRIPTS:
    [HideInInspector] TrainDeckManager cs_trainDeckManager;
    [SerializeField] GameObject go_trainDeckManager;
    [HideInInspector] TrainCard cs_trainCard;
    [SerializeField] GameObject go_trainCard;
    [HideInInspector] public ColourPileLogic[] cs_colourPileLogic = new ColourPileLogic[9];
    #endregion

    private void Awake()
    {
        cs_trainCard = go_trainCard.GetComponent<TrainCard>();
        cs_trainDeckManager = go_trainDeckManager.GetComponent<TrainDeckManager>();
    }

    private void Start()
    {
        for (int i = 0; i < cs_colourPileLogic.Length; i++)
        {
            cs_colourPileLogic[i] = colourPiles[i].GetComponent<ColourPileLogic>();
        }
    }
    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {

            if (transform.GetChild(i).tag == "trainCard" || transform.GetChild(i).tag == "marketTrainCard")
            {
                GameObject card = transform.GetChild(i).gameObject;
                string colour = card.GetComponent<TrainCard>().so_trainCards.colour;

                switch (colour)
                {
                    case "black":
                        card.transform.SetParent(colourPiles[0].transform);
                        card.transform.SetAsFirstSibling();
                        card.transform.position = colourPiles[0].transform.position;
                        break;
                    case "blue":
                        card.transform.SetParent(colourPiles[1].transform);
                        card.transform.SetAsFirstSibling();
                        card.transform.position = colourPiles[1].transform.position;
                        break;
                    case "green":
                        card.transform.SetParent(colourPiles[2].transform);
                        card.transform.SetAsFirstSibling();
                        card.transform.position = colourPiles[2].transform.position;
                        break;
                    case "pink":
                        card.transform.SetParent(colourPiles[3].transform);
                        card.transform.SetAsFirstSibling();
                        card.transform.position = colourPiles[3].transform.position;
                        break;
                    case "red":
                        card.transform.SetParent(colourPiles[4].transform);
                        card.transform.SetAsFirstSibling();
                        card.transform.position = colourPiles[4].transform.position;
                        break;
                    case "white":
                        card.transform.SetParent(colourPiles[5].transform);
                        card.transform.SetAsFirstSibling();
                        card.transform.position = colourPiles[5].transform.position;
                        break;
                    case "orange":
                        card.transform.SetParent(colourPiles[6].transform);
                        card.transform.SetAsFirstSibling();
                        card.transform.position = colourPiles[6].transform.position;
                        break;
                    case "yellow":
                        card.transform.SetParent(colourPiles[7].transform);
                        card.transform.SetAsFirstSibling();
                        card.transform.position = colourPiles[7].transform.position;
                        break;
                    case "loco":
                        card.transform.SetParent(colourPiles[8].transform);
                        card.transform.SetAsFirstSibling();
                        card.transform.position = colourPiles[8].transform.position;
                        break;

                }
            }
        }
    }

}
