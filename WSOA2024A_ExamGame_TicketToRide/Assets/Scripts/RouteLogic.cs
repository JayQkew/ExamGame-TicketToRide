using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        cs_playerManager1 = GameObject.Find("Player_1").GetComponent<PlayerManager>();
        cs_playerManager2 = GameObject.Find("Player_2").GetComponent<PlayerManager>();
        cs_trainDeckManager = GameObject.Find("TrainDeck").GetComponent<TrainDeckManager>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (cs_playerManager1.playerTurn)
        {
            ClaimRoute(cs_playerManager1);
        }
        else if (cs_playerManager2.playerTurn)
        {
            ClaimRoute(cs_playerManager2);
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
                if (cs_playerManagerCode.cs_colourPileLogic[i].numberOfCards >= so_routes.trainPieces)
                {
                    cs_playerManagerCode.trainPieces -= so_routes.trainPieces;
                    cs_playerManagerCode.points += so_routes.points;
                    cs_trainDeckManager.DiscardCard(cs_playerManagerCode.colourPiles[i].transform.GetChild(0).gameObject);
                }
            }
        }
    }
}
