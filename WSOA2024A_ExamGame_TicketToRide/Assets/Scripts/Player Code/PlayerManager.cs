using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    #region VARIABLES:
    [HideInInspector] public List<GameObject> trainHandCards = new List<GameObject>();
    [SerializeField] public List<GameObject> destinationHandCards = new List<GameObject>();
    [SerializeField] public List<GameObject> completedDestinationCards = new List<GameObject>();
    [SerializeField] public List<GameObject> colourPiles = new List<GameObject>(9);
    public Transform destinationHand;
    [SerializeField] public bool playerTurn;
    [SerializeField] public int trainPieces = 40;
    [SerializeField] public int points = 0;
    [SerializeField] public int privatePoints = 0;
    [SerializeField] public bool playerChoosingColour = false;
    #endregion

    #region COMPONENTS:
    [SerializeField] private TextMeshProUGUI tmp_trainPieces;
    [SerializeField] public Slider pointSlider;
    [SerializeField] public GameObject go_destinationCards;
    [SerializeField] public GameObject go_colourPiles;
    [SerializeField] public GameObject go_otherPlayer;
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

        if (gameObject.name == "Player_2")
        {
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        ColourPileManager();
        tmp_trainPieces.text = trainPieces.ToString();
        pointSlider.value = points;
        CheckLastRound();
    }

    private void PlayerTurnCheck()
    {
        if (playerTurn)
        {
            go_otherPlayer.SetActive(false);
        }
        else if (!playerTurn)
        {
            go_otherPlayer.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    public void EndTurnButton()
    {
        playerTurn = false;
        go_otherPlayer.GetComponent<PlayerManager>().playerTurn = true;
        PlayerTurnCheck();
    }

    private void ColourPileManager()
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

    public void CheckLastRound()
    {
        if(trainPieces <= 2)
        {
            Debug.Log("Play last round");
            GameManager.Instance.UpdateGameState(GameState.LastRound);
        }
    }

}
