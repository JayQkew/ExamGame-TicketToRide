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
    [SerializeField] ActionManager cs_actionManager;
    #endregion

    

    private void Awake()
    {
        cs_playerManager1 = GameObject.Find("Player_1").GetComponent<PlayerManager>();
        cs_playerManager2 = GameObject.Find("Player_2").GetComponent<PlayerManager>();
        cs_trainDeckManager = GameObject.Find("TrainDeck").GetComponent<TrainDeckManager>();
        cs_actionManager = GameObject.Find("Players").GetComponent<ActionManager>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if(cs_actionManager.canClaimRoute == true)
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
            cs_actionManager.amountRoutesClaimed++;
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
        }

    }
}
