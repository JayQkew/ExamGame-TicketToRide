using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationLogic : MonoBehaviour
{
    [SerializeField] public bool brokenOff_destination = false;
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
    }

    private void BreakOffCheck()
    {
        if(p1_claimedLocalRoutes.Count >= 3)
        {
            foreach (GameObject localRoute in p1_claimedLocalRoutes)
            {
                if (!localRoute.GetComponent<RouteLogic>().brokenOff_1)
                {
                    localRoute.GetComponent<RouteLogic>().brokenOff_1 = true;
                    brokenOff_destination = true;
                }
                localRoute.GetComponent<RouteLogic>().BreakOff2Check();
            }
        }

        if (p2_claimedLocalRoutes.Count >= 3)
        {
            foreach (GameObject localRoute in p2_claimedLocalRoutes)
            {
                if (!localRoute.GetComponent<RouteLogic>().brokenOff_1)
                {
                    localRoute.GetComponent<RouteLogic>().brokenOff_1 = true;
                    brokenOff_destination = true;
                }
                localRoute.GetComponent<RouteLogic>().BreakOff2Check();
            }
        }

    }
}
