using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RouteLogic : MonoBehaviour, IPointerClickHandler
{
    #region OTHER SCIRPTS:
    [SerializeField] SO_Routes so_routes;
    [SerializeField] PlayerManager cs_playerManager1;
    [SerializeField] PlayerManager cs_playerManager2;
    [SerializeField] TrainDeckManager cs_trainDeckManager;
    [SerializeField] ActionManager cs_actionManager;
    #endregion

    #region VARIABLES:
    [SerializeField] public bool isGreyRoute = false;
    [SerializeField] public bool routeClaimed = false;
    [SerializeField] public bool brokenOff_1 = false;
    [SerializeField] public bool brokenOff_2 = false;
    [SerializeField] public int trainPieces;
    [SerializeField] public int netTrainPieces = 0;
    [SerializeField] public bool _checked = false;
    [SerializeField] public bool _marked = false;

    [SerializeField] public List<GameObject> initial_Destinations = new List<GameObject>();

    [SerializeField] public List<GameObject> p1_connectedDestinations = new List<GameObject>();
    [SerializeField] public List<GameObject> p2_connectedDestinations = new List<GameObject>();
    [SerializeField] public List<GameObject> p1_subConnectedDestinations = new List<GameObject>();
    [SerializeField] public List<GameObject> p2_subConnectedDestinations = new List<GameObject>();

    [SerializeField] public List<GameObject> p1_connectedRoutes = new List<GameObject>();
    [SerializeField] public List<GameObject> p2_connectedRoutes = new List<GameObject>();
    [SerializeField] public List<GameObject> p1_subConnectedRoutes = new List<GameObject>();
    [SerializeField] public List<GameObject> p2_subConnectedRoutes = new List<GameObject>();
    #endregion

    private void Awake()
    {
        cs_playerManager1 = GameObject.Find("Player_1").GetComponent<PlayerManager>();
        cs_playerManager2 = GameObject.Find("Player_2").GetComponent<PlayerManager>();
        cs_trainDeckManager = GameObject.Find("TrainDeck").GetComponent<TrainDeckManager>();
        cs_actionManager = GameObject.Find("Players").GetComponent<ActionManager>();
    }

    private void Start()
    {
        trainPieces = so_routes.trainPieces;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (cs_actionManager.canClaimRoute == true)
        {
            if (!routeClaimed)
            {
                if (cs_playerManager1.playerTurn)
                {
                    ClaimRoute(cs_playerManager1);
                    cs_actionManager.amountRoutesClaimed++;
                }
                if (cs_playerManager2.playerTurn)
                {
                    ClaimRoute(cs_playerManager2);
                    cs_actionManager.amountRoutesClaimed++;
                }
            }
        }
    }

    public void ClaimRoute(PlayerManager cs_playerManagerCode) // checks if there are enough train pieces.
    {
        if (cs_playerManagerCode.trainPieces >= so_routes.trainPieces && cs_playerManagerCode.trainPieces > 2)
        {
            CardCheck(cs_playerManagerCode);
        }
    }

    private async void CardCheck(PlayerManager cs_playerManagerCode) // checks if the player has cards of that colour.
    {
        if (!isGreyRoute)
        {
            for (int i = 0; i < cs_playerManagerCode.cs_colourPileLogic.Length; i++) // loops through the array of colorpile scripts
            {
                if (cs_playerManagerCode.cs_colourPileLogic[i].colour == so_routes.colour) // checks if the colours match
                {
                    CardCheck2(cs_playerManagerCode, i);
                }
            }
        }
        else if (isGreyRoute)
        {
            Debug.Log("Chose grey");

            foreach (GameObject colourPile in cs_playerManagerCode.colourPiles) // loop though the colourPiles to see if they have been selected.
            {
                if (colourPile.GetComponent<ColourPileLogic>()._colourSelected == true) // if a card is selected
                {
                    CardCheck2(cs_playerManagerCode, cs_playerManagerCode.colourPiles.IndexOf(colourPile));
                }
            }
        }
    }

    private void CardCheck2(PlayerManager cs_playerManagerCode, int i) // checks if there are enough cards of that colour
    {
        int cardsLeft = so_routes.trainPieces - cs_playerManagerCode.cs_colourPileLogic[i].numberOfCards;
        if (cs_playerManagerCode.cs_colourPileLogic[i].numberOfCards >= so_routes.trainPieces) // checks if that colourpile has enough cards
        {
            cs_playerManagerCode.trainPieces -= so_routes.trainPieces;
            cs_playerManagerCode.points += so_routes.points;
            for (int j = 0; j < so_routes.trainPieces; j++)
            {
                cs_trainDeckManager.DiscardCard(cs_playerManagerCode.colourPiles[i].transform.GetChild(0).gameObject);
            }
            routeClaimed = true;
            ColourChange();
            QuickReset();
            InfoGathering();
            InfoSharing();
            DestinationCompletion();
            DestinationStateCheck();
            DestinationManagement();
        }
        else if (cs_playerManagerCode.cs_colourPileLogic[i].numberOfCards < so_routes.trainPieces && cs_playerManagerCode.colourPiles[8].transform.childCount >= cardsLeft - 1)
        {
            cs_playerManagerCode.trainPieces -= so_routes.trainPieces;
            cs_playerManagerCode.points += so_routes.points;
            Debug.Log("cards left: " + cardsLeft);
            for (int j = 0; j < cs_playerManagerCode.cs_colourPileLogic[i].numberOfCards; j++)
            {
                cs_trainDeckManager.DiscardCard(cs_playerManagerCode.colourPiles[i].transform.GetChild(0).gameObject);
            }
            for (int j = 0; j < cardsLeft; j++)
            {
                if (cs_playerManagerCode.colourPiles[8].transform.childCount > 1)
                {
                    cs_trainDeckManager.DiscardCard(cs_playerManagerCode.colourPiles[8].transform.GetChild(0).gameObject);

                }
            }
            routeClaimed = true;
            ColourChange();
            QuickReset();
            InfoGathering();
            InfoSharing();
            DestinationCompletion();
            DestinationStateCheck();
            DestinationManagement();
        }
        else
        {
            Debug.Log("Cant Do That BRUH");
        }

    }

    private void ColourChange()
    {
        if (cs_playerManager1.playerTurn)
        {
            GetComponent<SpriteRenderer>().color = Color.blue; // colour change for indication. *testing*
        }
        else if (cs_playerManager2.playerTurn)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "destination")
        {
            p1_connectedDestinations.Add(collision.gameObject);
            p2_connectedDestinations.Add(collision.gameObject);
            initial_Destinations.Add(collision.gameObject);
        }
    }

    private void DestinationStateCheck()
    {
        List<GameObject> endDestinations = new List<GameObject>();
        List<GameObject> allEndDestinations = new List<GameObject>();
        GameObject[] allDestinations = GameObject.FindGameObjectsWithTag("destination");

        foreach (GameObject dest in allDestinations)
        {
            if (dest.GetComponent<DestinationLogic>().p1_destinationState == DestinationStates.End || dest.GetComponent<DestinationLogic>().p2_destinationState == DestinationStates.End)
            {
                allEndDestinations.Add(dest);
            }
        }

        if (cs_playerManager1.playerTurn)
        {
            foreach (GameObject destination in p1_connectedDestinations)
            {
                if (destination.GetComponent<DestinationLogic>().p1_destinationState == DestinationStates.End)
                {
                    endDestinations.Add(destination);
                }
            }

        }
        else if (cs_playerManager2.playerTurn)
        {
            foreach (GameObject destination in p2_connectedDestinations)
            {
                if (destination.GetComponent<DestinationLogic>().p2_destinationState == DestinationStates.End)
                {
                    endDestinations.Add(destination);
                }
            }

        }
        foreach (GameObject endDestination in endDestinations)
        {
            if (cs_playerManager1.playerTurn)
            {
                foreach (GameObject destination in p1_connectedDestinations) // looping through alllll destinations and resetting them to !_start.
                {
                    if (destination.GetComponent<DestinationLogic>().p1_destinationState == DestinationStates.End) // finds end destinations
                    {
                        destination.GetComponent<DestinationLogic>()._start = false;
                        destination.GetComponent<DestinationLogic>()._checked = false;
                    }

                    if (destination.GetComponent<DestinationLogic>().p1_destinationState == DestinationStates.Linked)
                    {
                        destination.GetComponent<DestinationLogic>()._start = false;
                        destination.GetComponent<DestinationLogic>()._checked = false;
                    }

                    if (destination.GetComponent<DestinationLogic>().p1_destinationState == DestinationStates.Split)
                    {
                        destination.GetComponent<DestinationLogic>()._start = false;
                        destination.GetComponent<DestinationLogic>()._checked = false;
                    }

                    foreach (GameObject _route in destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes)
                    {
                        _route.GetComponent<RouteLogic>()._checked = false;
                        _route.GetComponent<RouteLogic>()._marked = false;
                    }
                }

            }
            else if (cs_playerManager2.playerTurn)
            {
                foreach (GameObject destination in p2_connectedDestinations) // looping through alllll destinations and resetting them to !_start.
                {
                    if (destination.GetComponent<DestinationLogic>().p2_destinationState == DestinationStates.End) // finds end destinations
                    {
                        destination.GetComponent<DestinationLogic>()._start = false;
                        destination.GetComponent<DestinationLogic>()._checked = false;
                    }

                    if (destination.GetComponent<DestinationLogic>().p2_destinationState == DestinationStates.Linked)
                    {
                        destination.GetComponent<DestinationLogic>()._start = false;
                        destination.GetComponent<DestinationLogic>()._checked = false;
                    }

                    if (destination.GetComponent<DestinationLogic>().p2_destinationState == DestinationStates.Split)
                    {
                        destination.GetComponent<DestinationLogic>()._start = false;
                        destination.GetComponent<DestinationLogic>()._checked = false;
                    }

                    foreach (GameObject _route in destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes)
                    {
                        _route.GetComponent<RouteLogic>()._checked = false;
                        _route.GetComponent<RouteLogic>()._marked = false;
                    }
                }

            }

            if (cs_playerManager1.playerTurn)
            {
                endDestination.GetComponent<DestinationLogic>()._start = true;
                endDestination.GetComponent<DestinationLogic>()._checked = true;
                endDestination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces = endDestination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>().trainPieces;
                endDestination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked = true;

            }
            else if (cs_playerManager2.playerTurn)
            {
                endDestination.GetComponent<DestinationLogic>()._start = true;
                endDestination.GetComponent<DestinationLogic>()._checked = true;
                endDestination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces = endDestination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>().trainPieces;
                endDestination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked = true;

            }

            if (cs_playerManager1.playerTurn)
            {
                foreach (GameObject destination in p1_connectedDestinations)
                {
                    if (destination.GetComponent<DestinationLogic>().p1_destinationState == DestinationStates.Linked && destination.GetComponent<DestinationLogic>()._checked == false)
                    {
                        if (destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked == true &&
                    destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[1].GetComponent<RouteLogic>()._checked == false)
                        {
                            destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[1].GetComponent<RouteLogic>().netTrainPieces = destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces;
                            destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[1].GetComponent<RouteLogic>().netTrainPieces += destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[1].GetComponent<RouteLogic>().trainPieces;
                            destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[1].GetComponent<RouteLogic>()._checked = true;
                        }
                        else if (destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[1].GetComponent<RouteLogic>()._checked == true &&
                            destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked == false)
                        {
                            destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces = destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[1].GetComponent<RouteLogic>().netTrainPieces;
                            destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces += destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>().trainPieces;
                            destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked = true;
                        }

                        if (destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[1].GetComponent<RouteLogic>()._checked == true &&
                            destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked == true)
                        {
                            destination.GetComponent<DestinationLogic>()._checked = true;
                        }


                    }
                    else if (destination.GetComponent<DestinationLogic>().p1_destinationState == DestinationStates.Split && !destination.GetComponent<DestinationLogic>()._checked)
                    {
                        List<bool> markedCheck = new List<bool>();
                        List<bool> routes_checkedCheck = new List<bool>();
                        foreach (GameObject route in destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes) // goes through each claimedLocalRoute
                        {
                            markedCheck.Add(route.GetComponent<RouteLogic>()._marked);//adds its marked bool to the markedCheck list;
                            routes_checkedCheck.Add(route.GetComponent<RouteLogic>()._checked); // adds its checked state to checkedCheck list;
                        }


                        foreach (GameObject route in destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes) // goes through each claimedLocalRoute
                        {
                            if (route.GetComponent<RouteLogic>()._checked) // if the claimedLocalRoute is already checked
                            {
                                if (!markedCheck.Contains(true)) // AND the markedCheck list doesnt contain a true;
                                {
                                    route.GetComponent<RouteLogic>()._marked = true; // make the claimedLocalRoute marked.
                                    destination.GetComponent<DestinationLogic>().p1_firstMarkedRoute.Insert(0, route);
                                    break; // stop the forloop.
                                }
                            }
                        }


                        foreach (GameObject route in destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes)
                        {
                            foreach (GameObject marked_route in destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes) // looking for marked route.
                            {
                                if (marked_route.GetComponent<RouteLogic>()._marked &&
                                    !route.GetComponent<RouteLogic>()._checked)
                                {
                                    route.GetComponent<RouteLogic>().netTrainPieces = marked_route.GetComponent<RouteLogic>().netTrainPieces;
                                    route.GetComponent<RouteLogic>().netTrainPieces += route.GetComponent<RouteLogic>().trainPieces;
                                    route.GetComponent<RouteLogic>()._checked = true;
                                }
                            }
                        }


                        if (!routes_checkedCheck.Contains(false))
                        {
                            destination.GetComponent<DestinationLogic>()._checked = true;
                        }

                        markedCheck.Clear();
                        routes_checkedCheck.Clear();
                    }
                }

            }
            else if (cs_playerManager2.playerTurn)
            {
                foreach (GameObject destination in p2_connectedDestinations)
                {
                    if (destination.GetComponent<DestinationLogic>().p2_destinationState == DestinationStates.Linked && destination.GetComponent<DestinationLogic>()._checked == false)
                    {
                        if (destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked == true &&
                    destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[1].GetComponent<RouteLogic>()._checked == false)
                        {
                            destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[1].GetComponent<RouteLogic>().netTrainPieces = destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces;
                            destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[1].GetComponent<RouteLogic>().netTrainPieces += destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[1].GetComponent<RouteLogic>().trainPieces;
                            destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[1].GetComponent<RouteLogic>()._checked = true;
                        }
                        else if (destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[1].GetComponent<RouteLogic>()._checked == true &&
                            destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked == false)
                        {
                            destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces = destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[1].GetComponent<RouteLogic>().netTrainPieces;
                            destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces += destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>().trainPieces;
                            destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked = true;
                        }

                        if (destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[1].GetComponent<RouteLogic>()._checked == true &&
                            destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked == true)
                        {
                            destination.GetComponent<DestinationLogic>()._checked = true;
                        }


                    }
                    else if (destination.GetComponent<DestinationLogic>().p2_destinationState == DestinationStates.Split && !destination.GetComponent<DestinationLogic>()._checked)
                    {
                        List<bool> markedCheck = new List<bool>();
                        List<bool> routes_checkedCheck = new List<bool>();
                        foreach (GameObject route in destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes) // goes through each claimedLocalRoute
                        {
                            markedCheck.Add(route.GetComponent<RouteLogic>()._marked);//adds its marked bool to the markedCheck list;
                            routes_checkedCheck.Add(route.GetComponent<RouteLogic>()._checked); // adds its checked state to checkedCheck list;
                        }


                        foreach (GameObject route in destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes) // goes through each claimedLocalRoute
                        {
                            if (route.GetComponent<RouteLogic>()._checked) // if the claimedLocalRoute is already checked
                            {
                                if (!markedCheck.Contains(true)) // AND the markedCheck list doesnt contain a true;
                                {
                                    route.GetComponent<RouteLogic>()._marked = true; // make the claimedLocalRoute marked.
                                    destination.GetComponent<DestinationLogic>().p2_firstMarkedRoute.Insert(0, route);
                                    break; // stop the forloop.
                                }
                            }
                        }


                        foreach (GameObject route in destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes)
                        {
                            foreach (GameObject marked_route in destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes) // looking for marked route.
                            {
                                if (marked_route.GetComponent<RouteLogic>()._marked &&
                                    !route.GetComponent<RouteLogic>()._checked)
                                {
                                    route.GetComponent<RouteLogic>().netTrainPieces = marked_route.GetComponent<RouteLogic>().netTrainPieces;
                                    route.GetComponent<RouteLogic>().netTrainPieces += route.GetComponent<RouteLogic>().trainPieces;
                                    route.GetComponent<RouteLogic>()._checked = true;
                                }
                            }
                        }


                        if (!routes_checkedCheck.Contains(false))
                        {
                            destination.GetComponent<DestinationLogic>()._checked = true;
                        }

                        markedCheck.Clear();
                        routes_checkedCheck.Clear();
                    }
                }

            }

            if (cs_playerManager1.playerTurn)
            {
                foreach (GameObject destination in p1_connectedDestinations)
                {
                    if (destination.GetComponent<DestinationLogic>().p1_destinationState == DestinationStates.Linked && !destination.GetComponent<DestinationLogic>()._checked)
                    {
                        if (destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked == true &&
                    destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[1].GetComponent<RouteLogic>()._checked == false)
                        {
                            destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[1].GetComponent<RouteLogic>().netTrainPieces = destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces;
                            destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[1].GetComponent<RouteLogic>().netTrainPieces += destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[1].GetComponent<RouteLogic>().trainPieces;
                            destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[1].GetComponent<RouteLogic>()._checked = true;
                        }
                        else if (destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[1].GetComponent<RouteLogic>()._checked == true &&
                                destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked == false)
                        {
                            destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces = destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[1].GetComponent<RouteLogic>().netTrainPieces;
                            destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces += destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>().trainPieces;
                            destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked = true;
                        }



                        destination.GetComponent<DestinationLogic>()._checked = true;

                    }

                    else if (destination.GetComponent<DestinationLogic>().p1_destinationState == DestinationStates.Split && !destination.GetComponent<DestinationLogic>()._checked)
                    {
                        foreach (GameObject route in destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes)
                        {
                            if (!route.GetComponent<RouteLogic>()._checked)
                            {
                                route.GetComponent<RouteLogic>().netTrainPieces = destination.GetComponent<DestinationLogic>().p1_firstMarkedRoute[0].GetComponent<RouteLogic>().netTrainPieces;
                                route.GetComponent<RouteLogic>().netTrainPieces += route.GetComponent<RouteLogic>().trainPieces;
                                route.GetComponent<RouteLogic>()._checked = true;

                            }
                        }


                        destination.GetComponent<DestinationLogic>()._checked = true;
                    }
                }
            }
            else if (cs_playerManager2.playerTurn)
            {
                foreach (GameObject destination in p2_connectedDestinations)
                {
                    if (destination.GetComponent<DestinationLogic>().p2_destinationState == DestinationStates.Linked && !destination.GetComponent<DestinationLogic>()._checked)
                    {
                        if (destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked == true &&
                    destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[1].GetComponent<RouteLogic>()._checked == false)
                        {
                            destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[1].GetComponent<RouteLogic>().netTrainPieces = destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces;
                            destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[1].GetComponent<RouteLogic>().netTrainPieces += destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[1].GetComponent<RouteLogic>().trainPieces;
                            destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[1].GetComponent<RouteLogic>()._checked = true;
                        }
                        else if (destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[1].GetComponent<RouteLogic>()._checked == true &&
                                destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked == false)
                        {
                            destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces = destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[1].GetComponent<RouteLogic>().netTrainPieces;
                            destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces += destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>().trainPieces;
                            destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked = true;
                        }



                        destination.GetComponent<DestinationLogic>()._checked = true;

                    }

                    else if (destination.GetComponent<DestinationLogic>().p2_destinationState == DestinationStates.Split && !destination.GetComponent<DestinationLogic>()._checked)
                    {
                        foreach (GameObject route in destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes)
                        {
                            if (!route.GetComponent<RouteLogic>()._checked)
                            {
                                route.GetComponent<RouteLogic>().netTrainPieces = destination.GetComponent<DestinationLogic>().p2_firstMarkedRoute[0].GetComponent<RouteLogic>().netTrainPieces;
                                route.GetComponent<RouteLogic>().netTrainPieces += route.GetComponent<RouteLogic>().trainPieces;
                                route.GetComponent<RouteLogic>()._checked = true;

                            }
                        }


                        destination.GetComponent<DestinationLogic>()._checked = true;
                    }
                }
            }

            if (cs_playerManager1.playerTurn)
            {
                foreach (GameObject destination in p1_connectedDestinations) // last foreach loop
                {
                    if (destination.GetComponent<DestinationLogic>().p1_destinationState == DestinationStates.End && destination.GetComponent<DestinationLogic>()._start == false)
                    {
                        destination.GetComponent<DestinationLogic>().p1_final_netTrainPieces = destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces;
                        destination.GetComponent<DestinationLogic>()._checked = true;
                        destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked = true;
                        if (destination.GetComponent<DestinationLogic>().p1_final_netTrainPieces > destination.GetComponent<DestinationLogic>().p1_longestRoute)
                        {
                            destination.GetComponent<DestinationLogic>().p1_longestRoute = destination.GetComponent<DestinationLogic>().p1_final_netTrainPieces;
                        }

                    }


                }

            }
            else if (cs_playerManager2.playerTurn)
            {
                foreach (GameObject destination in p2_connectedDestinations) // last foreach loop
                {
                    if (destination.GetComponent<DestinationLogic>().p2_destinationState == DestinationStates.End && destination.GetComponent<DestinationLogic>()._start == false)
                    {
                        destination.GetComponent<DestinationLogic>().p2_final_netTrainPieces = destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces;
                        destination.GetComponent<DestinationLogic>()._checked = true;
                        destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked = true;
                        if (destination.GetComponent<DestinationLogic>().p2_final_netTrainPieces > destination.GetComponent<DestinationLogic>().p2_longestRoute)
                        {
                            destination.GetComponent<DestinationLogic>().p2_longestRoute = destination.GetComponent<DestinationLogic>().p2_final_netTrainPieces;
                        }

                    }


                }

            }

            if (cs_playerManager1.playerTurn)
            {
                foreach (GameObject destination in p1_connectedDestinations)
                {
                    if (destination.GetComponent<DestinationLogic>().p1_destinationState == DestinationStates.Linked && !destination.GetComponent<DestinationLogic>()._checked)
                    {
                        if (destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[1].GetComponent<RouteLogic>()._checked == true &&
                    destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked == true)
                        {
                            destination.GetComponent<DestinationLogic>()._checked = true;
                        }

                    }

                    else if (destination.GetComponent<DestinationLogic>().p1_destinationState == DestinationStates.Split && !destination.GetComponent<DestinationLogic>()._checked)
                    {
                        destination.GetComponent<DestinationLogic>()._checked = true;
                    }

                }

            }
            else if (cs_playerManager2.playerTurn)
            {
                foreach (GameObject destination in p2_connectedDestinations)
                {
                    if (destination.GetComponent<DestinationLogic>().p2_destinationState == DestinationStates.Linked && !destination.GetComponent<DestinationLogic>()._checked)
                    {
                        if (destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[1].GetComponent<RouteLogic>()._checked == true &&
                    destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked == true)
                        {
                            destination.GetComponent<DestinationLogic>()._checked = true;
                        }

                    }

                    else if (destination.GetComponent<DestinationLogic>().p2_destinationState == DestinationStates.Split && !destination.GetComponent<DestinationLogic>()._checked)
                    {
                        destination.GetComponent<DestinationLogic>()._checked = true;
                    }

                }

            }

            if (cs_playerManager1.playerTurn)
            {
                foreach (GameObject _endDestination in endDestinations)
                {
                    if (_endDestination.GetComponent<DestinationLogic>().p1_longestRoute > endDestination.GetComponent<DestinationLogic>().p1_longestRoute)
                    {
                        endDestination.GetComponent<DestinationLogic>().p1_longestRoute = _endDestination.GetComponent<DestinationLogic>().p1_longestRoute;
                    }

                }

            }
            else if (cs_playerManager2.playerTurn)
            {
                foreach (GameObject _endDestination in endDestinations)
                {
                    if (_endDestination.GetComponent<DestinationLogic>().p2_longestRoute > endDestination.GetComponent<DestinationLogic>().p2_longestRoute)
                    {
                        endDestination.GetComponent<DestinationLogic>().p2_longestRoute = _endDestination.GetComponent<DestinationLogic>().p2_longestRoute;
                    }

                }

            }

            foreach (GameObject _endDestination in allEndDestinations)
            {
                foreach (GameObject dest in allEndDestinations)
                {
                    if (dest.GetComponent<DestinationLogic>().p1_longestRoute > _endDestination.GetComponent<DestinationLogic>().p1_longestRoute)
                    {
                        _endDestination.GetComponent<DestinationLogic>().p1_longestRoute = dest.GetComponent<DestinationLogic>().p1_longestRoute;
                    }

                    if (dest.GetComponent<DestinationLogic>().p2_longestRoute > _endDestination.GetComponent<DestinationLogic>().p2_longestRoute)
                    {
                        _endDestination.GetComponent<DestinationLogic>().p2_longestRoute = dest.GetComponent<DestinationLogic>().p2_longestRoute;
                    }
                }

                foreach (GameObject dest in allEndDestinations)
                {
                    if (dest.GetComponent<DestinationLogic>().p1_longestRoute > dest.GetComponent<DestinationLogic>().p2_longestRoute)
                    {
                        dest.GetComponent<DestinationLogic>()._longestRouteHolder = LongestRouteHolder.Player1;
                    }
                    else if (dest.GetComponent<DestinationLogic>().p2_longestRoute > dest.GetComponent<DestinationLogic>().p1_longestRoute)
                    {
                        dest.GetComponent<DestinationLogic>()._longestRouteHolder = LongestRouteHolder.Player2;
                    }
                }
            }
        }

        endDestinations.Clear();
    }
    private void QuickReset()
    {
        if (cs_playerManager1.playerTurn)
        {
            foreach (GameObject destination in p1_connectedDestinations)
            {
                destination.GetComponent<DestinationLogic>()._start = false;
                destination.GetComponent<DestinationLogic>()._checked = false;
            }
            foreach (GameObject route in p1_connectedRoutes)
            {
                route.GetComponent<RouteLogic>()._checked = false;
                route.GetComponent<RouteLogic>()._marked = false;
                route.GetComponent<RouteLogic>().netTrainPieces = 0;
            }

        }
        else if (cs_playerManager2.playerTurn)
        {
            foreach (GameObject destination in p2_connectedDestinations)
            {
                destination.GetComponent<DestinationLogic>()._start = false;
                destination.GetComponent<DestinationLogic>()._checked = false;
            }
            foreach (GameObject route in p2_connectedRoutes)
            {
                route.GetComponent<RouteLogic>()._checked = false;
                route.GetComponent<RouteLogic>()._marked = false;
                route.GetComponent<RouteLogic>().netTrainPieces = 0;
            }

        }
    }
    private void InfoGathering()
    {
        if (cs_playerManager1.playerTurn)
        {
            foreach (GameObject destination in p1_connectedDestinations)
            {
                p1_subConnectedDestinations.AddRange(destination.GetComponent<DestinationLogic>().p1_connectedDestintaions);
                p1_subConnectedRoutes.AddRange(destination.GetComponent<DestinationLogic>().p1_connectedRoutes);
                p1_subConnectedRoutes.Add(gameObject);
            }

            p1_connectedRoutes.AddRange(p1_subConnectedRoutes);
            p1_connectedDestinations.AddRange(p1_subConnectedDestinations);

            p1_subConnectedRoutes.Clear();
            p1_subConnectedDestinations.Clear();

        }
        else if (cs_playerManager2.playerTurn)
        {
            foreach (GameObject destination in p2_connectedDestinations)
            {
                p2_subConnectedDestinations.AddRange(destination.GetComponent<DestinationLogic>().p2_connectedDestintaions);
                p2_subConnectedRoutes.AddRange(destination.GetComponent<DestinationLogic>().p2_connectedRoutes);
                p2_subConnectedRoutes.Add(gameObject);
            }

            p2_connectedRoutes.AddRange(p2_subConnectedRoutes);
            p2_connectedDestinations.AddRange(p2_subConnectedDestinations);

            p2_subConnectedRoutes.Clear();
            p2_subConnectedDestinations.Clear();
        }
    }

    private void InfoSharing()
    {
        if (cs_playerManager1.playerTurn)
        {
            foreach (GameObject destination in p1_connectedDestinations)
            {
                var union_connectedRoutes = destination.GetComponent<DestinationLogic>().p1_connectedRoutes.Union(p1_connectedRoutes);
                var union_connectedDestinations = destination.GetComponent<DestinationLogic>().p1_connectedDestintaions.Union(p1_connectedDestinations);
                destination.GetComponent<DestinationLogic>().p1_connectedRoutes.Clear();
                destination.GetComponent<DestinationLogic>().p1_connectedDestintaions.Clear();

                foreach (GameObject _route in union_connectedRoutes)
                {
                    destination.GetComponent<DestinationLogic>().p1_connectedRoutes.Add(_route);
                }

                foreach (GameObject _destination in union_connectedDestinations)
                {
                    destination.GetComponent<DestinationLogic>().p1_connectedDestintaions.Add(_destination);
                }

                LongestRouteCheck(destination, destination.GetComponent<DestinationLogic>().p1_connectedDestintaions, destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes);
            }
        }
        else if (cs_playerManager2.playerTurn)
        {
            foreach (GameObject destination in p2_connectedDestinations)
            {
                var union_connectedRoutes = destination.GetComponent<DestinationLogic>().p2_connectedRoutes.Union(p2_connectedRoutes);
                var union_connectedDestinations = destination.GetComponent<DestinationLogic>().p2_connectedDestintaions.Union(p2_connectedDestinations);
                destination.GetComponent<DestinationLogic>().p2_connectedRoutes.Clear();
                destination.GetComponent<DestinationLogic>().p2_connectedDestintaions.Clear();


                foreach (GameObject _route in union_connectedRoutes)
                {
                    destination.GetComponent<DestinationLogic>().p2_connectedRoutes.Add(_route);
                }

                foreach (GameObject _destination in union_connectedDestinations)
                {
                    destination.GetComponent<DestinationLogic>().p2_connectedDestintaions.Add(_destination);
                }

                LongestRouteCheck(destination, destination.GetComponent<DestinationLogic>().p2_connectedDestintaions, destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes);
            }
        }
    }

    private void LongestRouteCheck(GameObject destination, List<GameObject> destinationList, List<GameObject> playerClaimedLocalRoutes)
    {
        foreach (GameObject _destination in destinationList) // foreach destination
        {
            _destination.GetComponent<DestinationLogic>().LocalRouteCheck();
        }

    }

    private void DestinationCompletion()
    {
        if (cs_playerManager1.playerTurn)
        {
            foreach (GameObject destination in p1_connectedDestinations)
            {
                DestinationCompletionPlayerCheck(cs_playerManager1, destination.GetComponent<DestinationLogic>().p1_connectedDestintaions, cs_playerManager1);
            }
        }
        else if (cs_playerManager2.playerTurn)
        {
            foreach (GameObject destination in p2_connectedDestinations)
            {
                DestinationCompletionPlayerCheck(cs_playerManager2, destination.GetComponent<DestinationLogic>().p2_connectedDestintaions, cs_playerManager2);
            }
        }
    }

    private void DestinationCompletionPlayerCheck(PlayerManager playerManager, List<GameObject> playerLinkedDestinations, PlayerManager player)
    {
        foreach (GameObject destinationCard in playerManager.destinationHandCards) // loops through players destinationCards.
        {
            DestinationCheck(playerManager, playerLinkedDestinations, destinationCard, player);
        }
    }

    private void DestinationCheck(PlayerManager playerManager, List<GameObject> playerLinkedDestinations, GameObject destinationCard, PlayerManager player)
    {
        foreach (GameObject destination in playerLinkedDestinations) // loops through list in the Destination list for the specific player.
        {
            Debug.Log(destination);
            if (destination.name == destinationCard.GetComponent<DestinationCard>().sO_DestinationTicket.to) // if the "to" of the destination card in the players hand matches the name of the linked Destination.
            {
                foreach (GameObject _destination in playerLinkedDestinations) // if yes, loop through the list again
                {
                    if (_destination.name == destinationCard.GetComponent<DestinationCard>().sO_DestinationTicket.from) // if the "from" matches another destination name
                    {
                        destinationCard.gameObject.GetComponent<DestinationCard>().cardComplete = true;
                        destinationCard.gameObject.GetComponent<Image>().color = Color.green;
                    }
                }
            }

            if (destination.name == destinationCard.GetComponent<DestinationCard>().sO_DestinationTicket.from)
            {
                foreach (GameObject _destination in playerLinkedDestinations)
                {
                    if (_destination.name == destinationCard.GetComponent<DestinationCard>().sO_DestinationTicket.to)
                    {
                        destinationCard.gameObject.GetComponent<DestinationCard>().cardComplete = true;
                        destinationCard.gameObject.GetComponent<Image>().color = Color.green;
                    }
                }
            }
        }

    }

    private void DestinationManagement()
    {
        foreach (GameObject destinationCard in cs_playerManager1.destinationHandCards)
        {
            if (destinationCard.GetComponent<DestinationCard>().cardComplete)
            {
                cs_playerManager1.completedDestinationCards.Add(destinationCard);
                cs_playerManager1.destinationHandCards.Remove(destinationCard);
            }
        }
        foreach (GameObject destinationCard in cs_playerManager2.destinationHandCards)
        {
            if (destinationCard.GetComponent<DestinationCard>().cardComplete)
            {
                cs_playerManager2.completedDestinationCards.Add(destinationCard);
                cs_playerManager2.destinationHandCards.Remove(destinationCard);
            }
        }
    }
}

public enum LongestRouteHolder
{
    None,
    Player1,
    Player2
}