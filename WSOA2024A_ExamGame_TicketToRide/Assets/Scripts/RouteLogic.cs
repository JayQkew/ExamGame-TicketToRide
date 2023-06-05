using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RouteLogic : MonoBehaviour, IPointerClickHandler
{
    #region OTHER SCIRPTS:
    [SerializeField] SO_Routes so_routes;
    [SerializeField] PlayerManager cs_playerManager;
    [SerializeField] TrainDeckManager cs_trainDeckManager;
    #endregion

    private void Awake()
    {
        cs_playerManager = GameObject.Find("Player_1").GetComponent<PlayerManager>();
        cs_trainDeckManager = GameObject.Find("TrainDeck").GetComponent <TrainDeckManager>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        ClaimRoute();
    }

    public void ClaimRoute()
    {
        if (cs_playerManager.trainPieces >= so_routes.trainPieces && cs_playerManager.trainPieces > 2)
        {
            CardCheck();
        }
    }

    private void CardCheck() // works!
    {
        for (int i = 0; i < cs_playerManager.cs_colourPileLogic.Length; i++)
        {
            if (cs_playerManager.cs_colourPileLogic[i].colour == so_routes.colour)
            {
                if (cs_playerManager.cs_colourPileLogic[i].numberOfCards >= so_routes.trainPieces)
                {
                    cs_playerManager.trainPieces -= so_routes.trainPieces;
                    cs_playerManager.points += so_routes.points;
                    cs_trainDeckManager.DiscardCard(cs_playerManager.colourPiles[i].transform.GetChild(0).gameObject);
                }
            }
        }

    }
}
