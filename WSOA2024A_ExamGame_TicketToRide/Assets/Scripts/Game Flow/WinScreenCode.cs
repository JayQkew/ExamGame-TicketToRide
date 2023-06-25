using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreenCode : MonoBehaviour
{
    #region TEXT CODE
    [Header("Text Code")]
    [SerializeField] public GameObject WinScreen;
    [SerializeField] public GameObject Player1Txt;
    [SerializeField] public GameObject Player2Txt;
    [SerializeField] public GameObject DrawTxt;
    [SerializeField] TextMeshProUGUI player1PointsTxt;
    [SerializeField] TextMeshProUGUI player2PointsTxt;
    [SerializeField] TextMeshProUGUI LongestRouteTxt;
    #endregion

    #region OTHER SCRIPTS
    [Header("Other Scripts")]
    [SerializeField] PlayerManager cs_player1Manager;
    [SerializeField] PlayerManager cs_player2Manager;
    #endregion

    #region VARIABLES
    [SerializeField] GameObject[] destinations;
    #endregion

    private void Start()
    {
        destinations = GameObject.FindGameObjectsWithTag("destination");

        WinScreen.SetActive(false);
        Player1Txt.SetActive(false);
        Player2Txt.SetActive(false);
        DrawTxt.SetActive(false);
    }

    public void CheckWinner()
    {
        int p1_longestRoute = 0;
        int p2_longestRoute = 0;

        //foreach (GameObject destination in destinations)
        //{
        //    if (destination.GetComponent<DestinationLogic>().p1_destinationState == DestinationStates.End && destination.GetComponent<DestinationLogic>().p1_destinationState == DestinationStates.End)
        //    {
        //        if (destination.GetComponent<DestinationLogic>().p1_longestRoute > p1_longestRoute)
        //        {
        //            p1_longestRoute = destination.GetComponent<DestinationLogic>().p1_longestRoute;
        //        }
        //        if (destination.GetComponent<DestinationLogic>().p2_longestRoute > p2_longestRoute)
        //        {
        //            p2_longestRoute = destination.GetComponent<DestinationLogic>().p2_longestRoute;
        //        }
        //    }
        //}


        //int player1Points = cs_player1Manager.points + cs_player1Manager.privatePoints;
        //int player2Points = cs_player2Manager.points + cs_player2Manager.privatePoints;

        //if (p1_longestRoute > p2_longestRoute)
        //{
        //    player1Points += 10;
        //    LongestRouteTxt.text = "Player 1!";
        //    Debug.Log("Player 1 got longest Route");
        //}
        //else if (p2_longestRoute > p1_longestRoute)
        //{
        //    player2Points += 10;
        //    LongestRouteTxt.text = "Player 2!";
        //    Debug.Log("Player 2 got longest Route");
        //}
        //else if (p1_longestRoute == p2_longestRoute)
        //{
        //    player1Points += 10;
        //    player2Points += 10;
        //    LongestRouteTxt.text = "Draw!";
        //}

        PlayerManager cs_playerManager1 = cs_player1Manager;
        PlayerManager cs_playerManager2 = cs_player2Manager;

        #region     PLAYER 1 PRIVATE POINTS
        if (cs_playerManager1.completedDestinationCards.Count >= 1)
        {
            foreach (GameObject completedDestination in cs_playerManager1.completedDestinationCards)
            {
                cs_playerManager1.privatePoints += completedDestination.GetComponent<DestinationCard>().sO_DestinationTicket.points;
                Debug.Log("P1 Points added: " + completedDestination.GetComponent<DestinationCard>().sO_DestinationTicket.points);
            }
        }
        if (cs_playerManager1.destinationHandCards.Count >= 1)
        {
            foreach (GameObject destinationCard in cs_playerManager1.destinationHandCards)
            {
                cs_playerManager1.privatePoints -= destinationCard.GetComponent<DestinationCard>().sO_DestinationTicket.points;
                Debug.Log("P1 Points deducted: " + destinationCard.GetComponent<DestinationCard>().sO_DestinationTicket.points);
            }
        }
        #endregion

        #region     PLAYER 2 PRIVATE POINTS
        if (cs_playerManager2.completedDestinationCards.Count >= 1)
        {
            foreach (GameObject completedDestination in cs_playerManager2.completedDestinationCards)
            {
                cs_playerManager2.privatePoints += completedDestination.GetComponent<DestinationCard>().sO_DestinationTicket.points;
                Debug.Log("P2 Points added: " + completedDestination.GetComponent<DestinationCard>().sO_DestinationTicket.points);
            }
        }
        if (cs_playerManager2.destinationHandCards.Count >= 1)
        {
            foreach (GameObject destinationCard in cs_playerManager2.destinationHandCards)
            {
                cs_playerManager2.privatePoints -= destinationCard.GetComponent<DestinationCard>().sO_DestinationTicket.points;
                Debug.Log("P2 Points deducted: " + destinationCard.GetComponent<DestinationCard>().sO_DestinationTicket.points);
            }
        }
        #endregion

        cs_playerManager1.points += cs_playerManager1.privatePoints;
        cs_playerManager2.points += cs_playerManager2.privatePoints;


        player1PointsTxt.text = cs_player1Manager.points.ToString();
        player2PointsTxt.text = cs_player2Manager.points.ToString();

        if (cs_player1Manager.points > cs_player2Manager.points)
        {
            Player1Txt.SetActive(true);
        }
        if ( cs_player2Manager.points > cs_player1Manager.points)
        {
            Player2Txt.SetActive(true);
        }
        if (cs_player1Manager.points == cs_player2Manager.points)
        {
            DrawTxt.SetActive(true);
        }
    }

    public void OpenWinScreen()
    {
        WinScreen.SetActive(true);
        CheckWinner();
    }
}
