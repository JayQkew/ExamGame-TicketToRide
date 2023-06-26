using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestinationLogic : MonoBehaviour
{
    [SerializeField] public LongestRouteHolder _longestRouteHolder;
    [SerializeField] public DestinationStates p1_destinationState;
    [SerializeField] public DestinationStates p2_destinationState;
    [SerializeField] public bool _start;
    [SerializeField] public bool _checked;
    [SerializeField] public int p1_final_netTrainPieces;
    [SerializeField] public int p2_final_netTrainPieces;
    [SerializeField] public int p1_longestRoute = 0;
    [SerializeField] public int p2_longestRoute = 0;

    [SerializeField] public List<GameObject> p1_connectedDestintaions = new List<GameObject>();
    [SerializeField] public List<GameObject> p2_connectedDestintaions = new List<GameObject>();

    [SerializeField] public List<GameObject> p1_connectedRoutes = new List<GameObject>();
    [SerializeField] public List<GameObject> p2_connectedRoutes = new List<GameObject>();

    [SerializeField] public List<GameObject> p1_claimedLocalRoutes = new List<GameObject>();
    [SerializeField] public List<GameObject> p2_claimedLocalRoutes = new List<GameObject>();

    [SerializeField] public List<GameObject> p1_firstMarkedRoute = new List<GameObject>();
    [SerializeField] public List<GameObject> p2_firstMarkedRoute = new List<GameObject>();

    [SerializeField] public List<GameObject> local_connectedRoutes = new List<GameObject>();

    private PlayerManager cs_playerManager1;
    private PlayerManager cs_playerManager2;
    private GameManager cs_gameManager;

    private void Awake()
    {
        cs_playerManager1 = GameObject.Find("Player_1").GetComponent<PlayerManager>();
        cs_playerManager2 = GameObject.Find("Player_2").GetComponent<PlayerManager>();
    }

    private void Start()
    {
        p1_destinationState = DestinationStates.Unclaimed;
        p2_destinationState = DestinationStates.Unclaimed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "route")
        {
            local_connectedRoutes.Add(collision.gameObject);
        }
    }

    public void LocalRouteCheck()
    {
        foreach (GameObject localRoute in local_connectedRoutes) // loops through local routes
        {
            if (localRoute.GetComponent<RouteLogic>().routeClaimed) // if the local route is claimed
            {
                if (cs_playerManager1.playerTurn && !p1_claimedLocalRoutes.Contains(localRoute) && !p2_claimedLocalRoutes.Contains(localRoute)) // check if its the players turn && if the list already contians the route
                {
                    p1_claimedLocalRoutes.Add(localRoute);
                }

                else if (cs_playerManager2.playerTurn && !p2_claimedLocalRoutes.Contains(localRoute) && !p1_claimedLocalRoutes.Contains(localRoute))
                {
                    p2_claimedLocalRoutes.Add(localRoute);
                }
            }
        }
        StateCheck();

    }

    private void StateCheck()
    {
        if (p1_claimedLocalRoutes.Count >= 3)
        {
            foreach (GameObject localRoute in p1_claimedLocalRoutes)
            {
                if (!localRoute.GetComponent<RouteLogic>().brokenOff_1)
                {
                    localRoute.GetComponent<RouteLogic>().brokenOff_1 = true;
                    p1_destinationState = DestinationStates.Split;
                }
            }
        }
        else if (p1_claimedLocalRoutes.Count == 2)
        {
            p1_destinationState = DestinationStates.Linked;
        }
        else if (p1_claimedLocalRoutes.Count == 1)
        {
            p1_destinationState = DestinationStates.End;
        }


        if (p2_claimedLocalRoutes.Count >= 3)
        {
            foreach (GameObject localRoute in p2_claimedLocalRoutes)
            {
                if (!localRoute.GetComponent<RouteLogic>().brokenOff_1)
                {
                    localRoute.GetComponent<RouteLogic>().brokenOff_1 = true;
                    p2_destinationState = DestinationStates.Split;
                }
            }
        }
        else if (p2_claimedLocalRoutes.Count == 2)
        {
            p2_destinationState = DestinationStates.Linked;
        }
        else if (p2_claimedLocalRoutes.Count == 1)
        {
            p2_destinationState = DestinationStates.End;
        }


    }
    public int LongestRoutePlayer()
    {
        int longestRoute = 0;
        if (cs_gameManager.hasGameEnded) // if the game ended 
        {
            if (p1_longestRoute > p2_longestRoute)
            {
                longestRoute = p1_longestRoute;
            }
            else if (p2_longestRoute > p1_longestRoute)
            {
                longestRoute = p2_longestRoute;
            }
        }

        return longestRoute;
    }

}


public enum DestinationStates
{
    Unclaimed,
    End,
    Linked,
    Split
}