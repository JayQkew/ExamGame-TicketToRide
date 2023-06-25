using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCompileScript : MonoBehaviour
{
    [SerializeField] PlayerManager cs_playerManager1;
    [SerializeField] PlayerManager cs_playerManager2;
    [SerializeField] GameManager cs_gameManager;

    private void Awake()
    {
        cs_playerManager1 = GameObject.Find("Player_1").GetComponent<PlayerManager>();
        cs_playerManager2 = GameObject.Find("Player_2").GetComponent<PlayerManager>();
        cs_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void ClickCalculations()
    {
        if (cs_gameManager.additionalTurns == 3)
        {
            Debug.Log("calculating");
            AddPrivatePoints();
        }
    }

    private void AddPrivatePoints()
    {
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
    }
}
