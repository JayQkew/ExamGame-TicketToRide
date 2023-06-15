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
                Debug.Log("here");
                ClaimRoute(cs_playerManager1);
            }
            if (cs_playerManager2.playerTurn)
            {
                Debug.Log("here");
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
}
