using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class RouteLogic : MonoBehaviour, IPointerClickHandler
{
    #region OTHER SCIRPTS:
    [SerializeField] SO_Routes so_routes;
    [SerializeField] PlayerManager cs_playerManager1;
    [SerializeField] PlayerManager cs_playerManager2;
    [SerializeField] TrainDeckManager cs_trainDeckManager;
    #endregion

    #region VARIABLES:
    [SerializeField] public bool routeClaimed = false;
    [SerializeField] public List<GameObject> _connectedDestinations = new List<GameObject>();
    [SerializeField] public List<GameObject> sub_connectedDestinations = new List<GameObject>();
    #endregion

    private void Awake()
    {
        cs_playerManager1 = GameObject.Find("Player_1").GetComponent<PlayerManager>();
        cs_playerManager2 = GameObject.Find("Player_2").GetComponent<PlayerManager>();
        cs_trainDeckManager = GameObject.Find("TrainDeck").GetComponent<TrainDeckManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!routeClaimed)
        {
            if (cs_playerManager1.playerTurn)
            {
                ClaimRoute(cs_playerManager1);
            }
            if (cs_playerManager2.playerTurn)
            {
                ClaimRoute(cs_playerManager2);
            }
        }

    }

    public void ClaimRoute(PlayerManager cs_playerManagerCode)
    {
        if (cs_playerManagerCode.trainPieces >= so_routes.trainPieces && cs_playerManagerCode.trainPieces > 2)
        {
            CardCheck(cs_playerManagerCode);
        }
    }

    private void CardCheck(PlayerManager cs_playerManagerCode) // works!
    {
        for (int i = 0; i < cs_playerManagerCode.cs_colourPileLogic.Length; i++)
        {
            if (cs_playerManagerCode.cs_colourPileLogic[i].colour == so_routes.colour)
            {
                CardCheck2(cs_playerManagerCode, i);
            }
        }
    }

    private void CardCheck2(PlayerManager cs_playerManagerCode, int i)
    {
        if (cs_playerManagerCode.cs_colourPileLogic[i].numberOfCards >= so_routes.trainPieces)
        {
            cs_playerManagerCode.trainPieces -= so_routes.trainPieces;
            cs_playerManagerCode.points += so_routes.points;
            for (int j = 0; j < so_routes.trainPieces; j++)
            {
                cs_trainDeckManager.DiscardCard(cs_playerManagerCode.colourPiles[i].transform.GetChild(0).gameObject);
            }
            GetComponent<SpriteRenderer>().color = Color.grey; // colour change for indication. *testing*
            InfoGathering();
            InfoSharing();
            DestinationCompletion();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _connectedDestinations.Add(collision.gameObject);
    }

    private void InfoGathering()
    {
        if (cs_playerManager1.playerTurn)
        {
            foreach (GameObject destinaiton in _connectedDestinations)
            {
                sub_connectedDestinations.AddRange(destinaiton.GetComponent<DestinationLogic>().p1_connectedDestintaions);
            }
            _connectedDestinations.AddRange(sub_connectedDestinations);
            sub_connectedDestinations.Clear();
        }
        else if (cs_playerManager2.playerTurn)
        {
            foreach (GameObject destinaiton in _connectedDestinations)
            {
                sub_connectedDestinations.AddRange(destinaiton.GetComponent<DestinationLogic>().p2_connectedDestintaions);
            }
            _connectedDestinations.AddRange(sub_connectedDestinations);
            sub_connectedDestinations.Clear();
        }
    }

    private void InfoSharing()
    {
        if (cs_playerManager1.playerTurn)
        {
            foreach (GameObject destination in _connectedDestinations)
            {
                var union_connectedDestinations = destination.GetComponent<DestinationLogic>().p1_connectedDestintaions.Union(_connectedDestinations);
                destination.GetComponent<DestinationLogic>().p1_connectedDestintaions.Clear();

                foreach (GameObject _destination in union_connectedDestinations)
                {
                    destination.GetComponent<DestinationLogic>().p1_connectedDestintaions.Add(_destination);
                }
            }
        }
        else if (cs_playerManager2.playerTurn)
        {
            foreach (GameObject destination in _connectedDestinations)
            {
                var union_connectedDestinations = destination.GetComponent<DestinationLogic>().p2_connectedDestintaions.Union(_connectedDestinations);
                destination.GetComponent<DestinationLogic>().p2_connectedDestintaions.Clear();

                foreach (GameObject _destination in union_connectedDestinations)
                {
                    destination.GetComponent<DestinationLogic>().p2_connectedDestintaions.Add(_destination);
                }
            }
        }
    }

    private void DestinationCompletion()
    {
        if (cs_playerManager1.playerTurn)
        {
            foreach (GameObject destination in _connectedDestinations)
            {
                DestinationCompletionPlayerCheck(cs_playerManager1, destination.GetComponent<DestinationLogic>().p1_connectedDestintaions, cs_playerManager1);
            }
        }
        else if (cs_playerManager2.playerTurn)
        {
            foreach (GameObject destination in _connectedDestinations)
            {
                DestinationCompletionPlayerCheck(cs_playerManager2, destination.GetComponent<DestinationLogic>().p2_connectedDestintaions, cs_playerManager2);
            }
        }
    }

    private void DestinationCompletionPlayerCheck(PlayerManager playerManager, List<GameObject> playerLinkedDestinations, PlayerManager player)
    {
        foreach (GameObject destinationCard in playerManager.destinationHandCards) // loops through players destinationCards.
        {
            DestinationCheck(playerManager, playerLinkedDestinations, destinationCard, player);
        }
    }

    private void DestinationCheck(PlayerManager playerManager, List<GameObject> playerLinkedDestinations, GameObject destinationCard, PlayerManager player)
    {
        foreach (GameObject destination in playerLinkedDestinations) // loops through list in the Destination list for the specific player.
        {
            Debug.Log(destination);
            if (destination.name == destinationCard.GetComponent<DestinationCard>().sO_DestinationTicket.to) // if the "to" of the destination card in the players hand matches the name of the linked Destination.
            {
                foreach (GameObject _destination in playerLinkedDestinations) // if yes, loop through the list again
                {
                    if (_destination.name == destinationCard.GetComponent<DestinationCard>().sO_DestinationTicket.from) // if the "from" matches another destination name
                    {
                        destinationCard.gameObject.GetComponent<DestinationCard>().cardComplete = true;
                    }
                }
            }

            if (destination.name == destinationCard.GetComponent<DestinationCard>().sO_DestinationTicket.from)
            {
                foreach(GameObject _destination in playerLinkedDestinations)
                {
                    if (_destination.name == destinationCard.GetComponent<DestinationCard>().sO_DestinationTicket.to)
                    {
                        destinationCard.gameObject.GetComponent<DestinationCard>().cardComplete = true;
                    }
                }
            }
        }

    }
}
