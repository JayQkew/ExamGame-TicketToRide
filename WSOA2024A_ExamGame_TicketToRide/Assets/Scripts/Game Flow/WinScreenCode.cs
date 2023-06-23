using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenCode : MonoBehaviour
{
    #region TEXT CODE
    [Header("Text Code")]
    [SerializeField] public GameObject WinScreen;
    [SerializeField] public GameObject Player1Txt;
    [SerializeField] public GameObject Player2Txt;
    [SerializeField] public GameObject DrawTxt;
    #endregion

    #region OTHER SCRIPTS
    [Header("Other Scripts")]
    [SerializeField] PlayerManager cs_player1Manager;
    [SerializeField] PlayerManager cs_player2Manager;
    #endregion

    private void Start()
    {
        WinScreen.SetActive(false);
        Player1Txt.SetActive(false);
        Player2Txt.SetActive(false);
        DrawTxt.SetActive(false);
    }

    public void CheckWinner()
    {
        int player1Points = cs_player1Manager.points + cs_player1Manager.privatePoints;
        int player2Points = cs_player2Manager.points + cs_player2Manager.privatePoints;

        if (player1Points > player2Points)
        {
            Player1Txt.SetActive(true);
        }
        if ( player2Points > player1Points)
        {
            Player2Txt.SetActive(true);
        }
        if (player1Points == player2Points)
        {
            DrawTxt.SetActive(true);
        }
    }
}
