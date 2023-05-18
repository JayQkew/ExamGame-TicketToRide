using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DestinationCard : MonoBehaviour, IPointerClickHandler
{
    #region OTHER SCRIPTS:
    [SerializeField] SO_DestinationTicket sO_DestinationTicket;
    [SerializeField] UI_PlayerManager cs_playerManager;
    [SerializeField] DestinationDeck cs_destinationDeck;
    #endregion

    #region GAMEOBJECTS:
    #endregion

    #region OTHER COMPONENTS:
    [SerializeField] TextMeshProUGUI tmp_from;
    [SerializeField] TextMeshProUGUI tmp_to;
    [SerializeField] TextMeshProUGUI tmp_points;
    #endregion

    private void Awake()
    {
        #region GETTING OTHER SCRIPTS:
        cs_playerManager = GameObject.Find("Player_1").GetComponent<UI_PlayerManager>();

        cs_destinationDeck = GameObject.Find("DestinationsDeck").GetComponent<DestinationDeck>();
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
        switch(transform.parent.name)
        {
            case "choice1":
            case "choice2":
            case "choice3":
                cs_playerManager.destinationHandCards.Add(eventData.pointerClick); // adds it to the players list of destination cards
                eventData.pointerClick.transform.SetParent(cs_playerManager.destinationHand); // sets it parent to the destinationHand of the player (to be sorted)
                eventData.pointerClick.transform.position = cs_playerManager.destinationHand.transform.position; // transforms its position to the destinationHand
                break;
        }
    }
}
