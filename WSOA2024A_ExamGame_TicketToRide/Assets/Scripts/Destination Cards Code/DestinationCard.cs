using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class DestinationCard : MonoBehaviour, IPointerClickHandler
{
    #region VARIABLES:
    [SerializeField] public bool cardComplete = false;
    #endregion

    #region OTHER SCRIPTS:
    [SerializeField] public SO_DestinationTicket sO_DestinationTicket;
    [SerializeField] PlayerManager cs_playerManager1;
    [SerializeField] PlayerManager cs_playerManager2;
    [SerializeField] DestinationDeck cs_destinationDeck;
    [SerializeField] GameManager cs_gameManager;
    #endregion

    #region GAMEOBJECTS:
    [SerializeField] GameObject doneButton;
    #endregion

    #region OTHER COMPONENTS:
    [SerializeField] TextMeshProUGUI tmp_from;
    [SerializeField] TextMeshProUGUI tmp_to;
    [SerializeField] TextMeshProUGUI tmp_points;
    #endregion

    private void Awake()
    {
        #region GETTING OTHER SCRIPTS:
        cs_playerManager1 = GameObject.Find("Player_1").GetComponent<PlayerManager>();

        cs_playerManager2 = GameObject.Find("Player_2").GetComponent<PlayerManager>();

        cs_destinationDeck = GameObject.Find("DestinationsDeck").GetComponent<DestinationDeck>();

        cs_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        #endregion
    }
    void Start()
    {
        tmp_from.text = sO_DestinationTicket.from;
        tmp_to.text = sO_DestinationTicket.to;
        tmp_points.text = sO_DestinationTicket.points.ToString();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (cs_playerManager1.playerTurn)
        {
            PlayerCheck(eventData, cs_playerManager1);
        }
        else if (cs_playerManager2.playerTurn)
        {
            PlayerCheck(eventData, cs_playerManager2);
        }
    }

    private void PlayerCheck(PointerEventData eventData, PlayerManager cs_playerManagerCode)
    {
        switch (transform.parent.name)
        {
            case "choice1":
            case "choice2":
            case "choice3":
                cs_playerManagerCode.destinationHandCards.Add(eventData.pointerClick); // adds it to the players list of destination cards
                eventData.pointerClick.transform.SetParent(cs_playerManagerCode.destinationHand); // sets it parent to the destinationHand of the player (to be sorted)
                eventData.pointerClick.transform.position = cs_playerManagerCode.destinationHand.transform.position; // transforms its position to the destinationHand
                cs_destinationDeck.cardsChosen += 1;
                break;
        }

        if (cs_destinationDeck.cardsChosen >= 1 && cs_gameManager.currentTurn > 1)
        {
            cs_destinationDeck.doneButton.SetActive(true);
        }

        if (cs_destinationDeck.cardsChosen >= 2 && cs_gameManager.currentTurn <= 1)
        {
            cs_destinationDeck.doneButton.SetActive(true);
        }


    }
}
