using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestinationLogic : MonoBehaviour
{
    [SerializeField] public DestinationStates destinationState;
    [SerializeField] public bool _start;
    [SerializeField] public bool _checked;
    [SerializeField] public int p1_final_netTrainPieces;
    [SerializeField] public int p2_final_netTrainPieces;

    [SerializeField] public List<GameObject> p1_connectedDestintaions = new List<GameObject>();
    [SerializeField] public List<GameObject> p2_connectedDestintaions = new List<GameObject>();

    [SerializeField] public List<GameObject> p1_connectedRoutes = new List<GameObject>();
    [SerializeField] public List<GameObject> p2_connectedRoutes = new List<GameObject>();

    [SerializeField] public List<GameObject> p1_claimedLocalRoutes = new List<GameObject>();
    [SerializeField] public List<GameObject> p2_claimedLocalRoutes = new List<GameObject>();

    [SerializeField] public List<GameObject> local_connectedRoutes = new List<GameObject>();

    private PlayerManager cs_playerManager1;
    private PlayerManager cs_playerManager2;

    private void Awake()
    {
        cs_playerManager1 = GameObject.Find("Player_1").GetComponent<PlayerManager>();
        cs_playerManager2 = GameObject.Find("Player_2").GetComponent<PlayerManager>();
    }

    private void Start()
    {
        destinationState = DestinationStates.Unclaimed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        local_connectedRoutes.Add(collision.gameObject);
    }

    public void LocalRouteCheck()
    {
        foreach (GameObject localRoute in local_connectedRoutes) // loops through local routes
        {
            if (localRoute.GetComponent<RouteLogic>().routeClaimed) // if the local route is claimed
            {
                if (cs_playerManager1.playerTurn && !p1_claimedLocalRoutes.Contains(localRoute)) // check if its the players turn && if the list already contians the route
                {
                    p1_claimedLocalRoutes.Add(localRoute);
                }

                else if (cs_playerManager2.playerTurn && !p2_claimedLocalRoutes.Contains(localRoute))
                {
                    p2_claimedLocalRoutes.Add(localRoute);
                }
            }
        }
        BreakOffCheck();
        StateCheck();

        //if (cs_playerManager1.playerTurn)
        //{
        //    foreach (GameObject claimedLocalRoute in p1_claimedLocalRoutes)
        //    {
        //        //claimedLocalRoute.GetComponent<RouteLogic>()._LongestRouteCheck(p1_claimedLocalRoutes);
        //    }
        //}
        //else if (cs_playerManager2.playerTurn)
        //{
        //    foreach (GameObject claimedLocalRoute in p2_claimedLocalRoutes)
        //    {
        //        //claimedLocalRoute.GetComponent<RouteLogic>()._LongestRouteCheck(p2_claimedLocalRoutes);
        //    }
        //}
    }

    private void BreakOffCheck()
    {
        if (p1_claimedLocalRoutes.Count >= 3)
        {
            foreach (GameObject localRoute in p1_claimedLocalRoutes)
            {
                if (!localRoute.GetComponent<RouteLogic>().brokenOff_1)
                {
                    localRoute.GetComponent<RouteLogic>().brokenOff_1 = true;
                    destinationState = DestinationStates.Split;
                }
                localRoute.GetComponent<RouteLogic>().BreakOff2Check();
            }
        }
        else if (p1_claimedLocalRoutes.Count == 2)
        {
            destinationState = DestinationStates.Linked;
        }
        else if (p1_claimedLocalRoutes.Count == 1)
        {
            destinationState = DestinationStates.End;
        }


        if (p2_claimedLocalRoutes.Count >= 3)
        {
            foreach (GameObject localRoute in p2_claimedLocalRoutes)
            {
                if (!localRoute.GetComponent<RouteLogic>().brokenOff_1)
                {
                    localRoute.GetComponent<RouteLogic>().brokenOff_1 = true;
                    destinationState = DestinationStates.Split;
                }
                localRoute.GetComponent<RouteLogic>().BreakOff2Check();
            }
        }

        foreach ( GameObject claimedRoute in p1_claimedLocalRoutes)
        {
            //claimedRoute.GetComponent<RouteLogic>()._checked = false;
        }

        foreach (GameObject claimedRoute in p2_claimedLocalRoutes)
        {
            //claimedRoute.GetComponent<RouteLogic>()._checked = false;
        }

    }

    private void StateCheck()
    {
        List<GameObject> claimedLocalRoutes = new List<GameObject>();

        if (cs_playerManager1.playerTurn)
        {
            claimedLocalRoutes = p1_claimedLocalRoutes;
        }
        else if (cs_playerManager2.playerTurn)
        {
            claimedLocalRoutes = p2_claimedLocalRoutes;
        }

        switch (destinationState)
        {
            case DestinationStates.End:
                EndDestination(claimedLocalRoutes);
                break;
            case DestinationStates.Linked:
                LinkedDestination(claimedLocalRoutes);
                break;
            case DestinationStates.Split:
                SplitDestination(claimedLocalRoutes);
                break;
        }
    }

    private void EndDestination(List<GameObject> claimedLocalRoutes)
    {
        //if (_start)
        //{
        //    claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces += claimedLocalRoutes[0].GetComponent<RouteLogic>().trainPieces;
        //}
        //else if (!_start)
        //{
        //    final_netTrainPieces = claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces;
        //}
    }
    private void LinkedDestination(List<GameObject> claimedLocalRoutes)
    {
        _start = false;
    }

    private void SplitDestination(List<GameObject> claimedLocalRoutes)
    {

    }
}

public enum DestinationStates
{
    Unclaimed,
    End,
    Linked,
    Split
}