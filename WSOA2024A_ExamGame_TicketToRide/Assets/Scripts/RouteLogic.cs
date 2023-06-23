using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
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
    [SerializeField] public List<GameObject> _connectedDestinations = new List<GameObject>();
    [SerializeField] public List<GameObject> sub_connectedDestinations = new List<GameObject>();

    [SerializeField] public List<GameObject> _connectedRoutes = new List<GameObject>();
    [SerializeField] public List<GameObject> sub_connectedRoutes = new List<GameObject>();
    #endregion

    private void Awake()
    {
        cs_playerManager1 = GameObject.Find("Player_1").GetComponent<PlayerManager>();
        cs_playerManager2 = GameObject.Find("Player_2").GetComponent<PlayerManager>();
        cs_trainDeckManager = GameObject.Find("TrainDeck").GetComponent<TrainDeckManager>();
    }

    private void Start()
    {
        trainPieces = so_routes.trainPieces;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!routeClaimed)
        {
            if (cs_playerManager1.playerTurn)
            {
                ClaimRoute(cs_playerManager1);
            }
            if (cs_playerManager2.playerTurn)
            {
                ClaimRoute(cs_playerManager2);
            }
        }

    }

    public void BreakOff2Check()
    {
        int numBrokenOff_destination = 0;
        foreach (GameObject destination in initial_Destinations)
        {
            if (destination.GetComponent<DestinationLogic>().destinationState == DestinationStates.Split)
            {
                numBrokenOff_destination++;
            }
        }
        if (numBrokenOff_destination == 2)
        {
            brokenOff_2 = true;
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
            cs_playerManagerCode.playerChoosingColour = true;
            foreach (GameObject colourPile in cs_playerManagerCode.colourPiles)
            {
                colourPile.GetComponent<ColourPileLogic>().colourSelect = true;
            }

            await Task.Delay(2500); // wait 3 seconds before exicuting next line.

            foreach (GameObject colourPile in cs_playerManagerCode.colourPiles) // loop though the colourPiles to see if they have been selected.
            {
                if (colourPile.GetComponent<ColourPileLogic>()._colourSelected == true) // if a card is selected
                {
                    CardCheck2(cs_playerManagerCode, cs_playerManagerCode.colourPiles.IndexOf(colourPile));
                    cs_playerManagerCode.playerChoosingColour = false;
                }
            }
        }
    }

    private async void CardCheck2(PlayerManager cs_playerManagerCode, int i) // checks if there are enough cards of that colour
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
            GetComponent<SpriteRenderer>().color = Color.grey; // colour change for indication. *testing*
            QuickReset();
            InfoGathering();
            InfoSharing();
            DestinationCompletion();
            DestinationStateCheck();
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
            GetComponent<SpriteRenderer>().color = Color.grey; // colour change for indication. *testing*
            QuickReset();
            InfoGathering();
            InfoSharing();
            DestinationCompletion();
            DestinationStateCheck();
        }
        else
        {
            Debug.Log("Cant Do That BRUH");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _connectedDestinations.Add(collision.gameObject);
        initial_Destinations.Add(collision.gameObject);
    }

    private void DestinationStateCheck()
    {
        foreach (GameObject destination in _connectedDestinations) // looping through alllll destinations and resetting them to !_start.
        {
            if (destination.GetComponent<DestinationLogic>().destinationState == DestinationStates.End) // finds end destinations
            {
                destination.GetComponent<DestinationLogic>()._start = false;
                destination.GetComponent<DestinationLogic>()._checked = false;
            }

            if (destination.GetComponent<DestinationLogic>().destinationState == DestinationStates.Linked)
            {
                destination.GetComponent<DestinationLogic>()._start = false;
                destination.GetComponent<DestinationLogic>()._checked = false;
            }

            if (destination.GetComponent<DestinationLogic>().destinationState == DestinationStates.Split)
            {
                destination.GetComponent<DestinationLogic>()._start = false;
                destination.GetComponent<DestinationLogic>()._checked = false;
            }

            if (cs_playerManager1.playerTurn)
            {
                foreach (GameObject _route in destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes)
                {
                    _route.GetComponent<RouteLogic>()._checked = false;
                }
            }
            else if (cs_playerManager2.playerTurn)
            {
                foreach (GameObject _route in destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes)
                {
                    _route.GetComponent<RouteLogic>()._checked = false;
                }
            }
        }


        foreach (GameObject destination in _connectedDestinations) // looping through alllll destinations until only one is selected to _start.
        {
            if (destination.GetComponent<DestinationLogic>().destinationState == DestinationStates.End && destination.GetComponent<DestinationLogic>()._checked == false) // finds first end destination and makes it the _start.
            {
                destination.GetComponent<DestinationLogic>()._start = true;
                destination.GetComponent<DestinationLogic>()._checked = true;
                destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces = destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>().trainPieces;
                destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked = true;
                break; // stops the forloop so that two or more dont get the _start turned true
            }
        }


        foreach (GameObject destination in _connectedDestinations)
        {
            if (destination.GetComponent<DestinationLogic>().destinationState == DestinationStates.Linked && destination.GetComponent<DestinationLogic>()._checked == false)
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
            else if (destination.GetComponent<DestinationLogic>().destinationState == DestinationStates.Split && destination.GetComponent<DestinationLogic>()._checked == false)
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
                            destination.GetComponent<DestinationLogic>().p1_firstMarkedRoute.Add(route);
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

        foreach (GameObject destination in _connectedDestinations)
        {
            if (destination.GetComponent<DestinationLogic>().destinationState == DestinationStates.Linked && !destination.GetComponent<DestinationLogic>()._checked)
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

            else if (destination.GetComponent<DestinationLogic>().destinationState == DestinationStates.Split && !destination.GetComponent<DestinationLogic>()._checked)
            {
                List<bool> markedCheck = new List<bool>();
                List<bool> routes_checkedCheck = new List<bool>();

                foreach (GameObject route in destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes)
                {
                    if (!route.GetComponent<RouteLogic>()._checked)
                    {
                        route.GetComponent<RouteLogic>().netTrainPieces = destination.GetComponent<DestinationLogic>().p1_firstMarkedRoute[0].GetComponent<RouteLogic>().netTrainPieces;
                        route.GetComponent<RouteLogic>().netTrainPieces += route.GetComponent<RouteLogic>().trainPieces;
                        route.GetComponent<RouteLogic>()._checked = true;

                    }
                }

                destination.GetComponent <DestinationLogic>()._checked = true;
            }
        }


        foreach (GameObject destination in _connectedDestinations) // last foreach loop
        {
            if (destination.GetComponent<DestinationLogic>().destinationState == DestinationStates.End && destination.GetComponent<DestinationLogic>()._start == false)
            {
                if (cs_playerManager1.playerTurn)
                {
                    destination.GetComponent<DestinationLogic>().p1_final_netTrainPieces = destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces;
                    destination.GetComponent<DestinationLogic>()._checked = true;
                    destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked = true;


                }
                else if (cs_playerManager2.playerTurn)
                {
                    destination.GetComponent<DestinationLogic>().p2_final_netTrainPieces = destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>().netTrainPieces;
                    destination.GetComponent<DestinationLogic>()._checked = true;
                    destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked = true;
                }
            }

        }

        foreach (GameObject destination in _connectedDestinations)
        {
            if (destination.GetComponent<DestinationLogic>().destinationState == DestinationStates.Linked && !destination.GetComponent<DestinationLogic>()._checked)
            {
                if (destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[1].GetComponent<RouteLogic>()._checked == true &&
                    destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes[0].GetComponent<RouteLogic>()._checked == true)
                {
                    destination.GetComponent<DestinationLogic>()._checked = true;
                }

            }

            else if (destination.GetComponent<DestinationLogic>().destinationState == DestinationStates.Split && !destination.GetComponent<DestinationLogic>()._checked)
            {
                destination.GetComponent<DestinationLogic>()._checked = true;
            }

            destination.GetComponent<DestinationLogic>().p1_firstMarkedRoute.Clear();
        }


    }

    private void QuickReset()
    {
        foreach (GameObject destination in _connectedDestinations)
        {
            destination.GetComponent<DestinationLogic>()._start = false;
            destination.GetComponent<DestinationLogic>()._checked = false;
        }
        foreach (GameObject route in _connectedRoutes)
        {
            route.GetComponent<RouteLogic>()._checked = false;
            route.GetComponent<RouteLogic>().netTrainPieces = 0;
        }
    }
    private void InfoGathering()
    {
        if (cs_playerManager1.playerTurn)
        {
            foreach (GameObject destination in _connectedDestinations)
            {
                sub_connectedDestinations.AddRange(destination.GetComponent<DestinationLogic>().p1_connectedDestintaions);
                sub_connectedRoutes.AddRange(destination.GetComponent<DestinationLogic>().p1_connectedRoutes);
                sub_connectedRoutes.Add(gameObject);

            }

            _connectedRoutes.AddRange(sub_connectedRoutes);
            _connectedDestinations.AddRange(sub_connectedDestinations);

            sub_connectedRoutes.Clear();
            sub_connectedDestinations.Clear();

        }
        else if (cs_playerManager2.playerTurn)
        {
            foreach (GameObject destination in _connectedDestinations)
            {
                sub_connectedRoutes.AddRange(destination.GetComponent<DestinationLogic>().p2_connectedRoutes);
                sub_connectedDestinations.AddRange(destination.GetComponent<DestinationLogic>().p2_connectedDestintaions);
                sub_connectedRoutes.Add(gameObject);
            }

            _connectedRoutes.AddRange(sub_connectedRoutes);
            _connectedDestinations.AddRange(sub_connectedDestinations);

            sub_connectedRoutes.Clear();
            sub_connectedDestinations.Clear();
        }
    }

    private void InfoSharing()
    {
        if (cs_playerManager1.playerTurn)
        {
            foreach (GameObject destination in _connectedDestinations)
            {
                var union_connectedRoutes = destination.GetComponent<DestinationLogic>().p1_connectedRoutes.Union(_connectedRoutes);
                var union_connectedDestinations = destination.GetComponent<DestinationLogic>().p1_connectedDestintaions.Union(_connectedDestinations);
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
            foreach (GameObject destination in _connectedDestinations)
            {
                var union_connectedRoutes = destination.GetComponent<DestinationLogic>().p2_connectedRoutes.Union(_connectedRoutes);
                var union_connectedDestinations = destination.GetComponent<DestinationLogic>().p2_connectedDestintaions.Union(_connectedDestinations);
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

    private void LongestRouteCheck(GameObject destination, List<GameObject> destinationList, List<GameObject> playerClaimedLocalRoutes) // second player gets all already collected routes bug.
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
            foreach (GameObject destination in _connectedDestinations)
            {
                DestinationCompletionPlayerCheck(cs_playerManager1, destination.GetComponent<DestinationLogic>().p1_connectedDestintaions, cs_playerManager1);
            }
        }
        else if (cs_playerManager2.playerTurn)
        {
            foreach (GameObject destination in _connectedDestinations)
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
                    }
                }
            }
        }

    }

    public void _LongestRouteCheck(List<GameObject> playerClaimedLocalRoutes)
    {
        foreach (GameObject destination in initial_Destinations)
        {
            if (cs_playerManager1.playerTurn)
            {
                //RecursionCheck(destination, destination.GetComponent<DestinationLogic>().p1_claimedLocalRoutes);
            }
            else if (cs_playerManager2.playerTurn)
            {
                //RecursionCheck(destination, destination.GetComponent<DestinationLogic>().p2_claimedLocalRoutes);
            }
        }
    }

    //private GameObject RecursionCheck(GameObject destination, List<GameObject> playerClaimedLocalRoutes)
    //{
    //    GameObject recursionObject = null;
    //    if (destination.GetComponent<DestinationLogic>().split_destination)
    //    {
    //        foreach (GameObject route in playerClaimedLocalRoutes)
    //        {
    //            if (!gameObject)
    //            {
    //                foreach (GameObject _destination in route.GetComponent<RouteLogic>().initial_Destinations)
    //                {
    //                    Debug.Log("yo");
    //                    recursionObject = RecursionCheck(_destination, playerClaimedLocalRoutes);
    //                    return RecursionCheck(_destination, playerClaimedLocalRoutes);
    //                }
    //            }
    //        }
    //    }
    //    else if (destination.GetComponent<DestinationLogic>().linked_destination)
    //    {
    //        foreach (GameObject route in playerClaimedLocalRoutes)
    //        {
    //            if (!gameObject)
    //            {
    //                foreach (GameObject _destination in route.GetComponent<RouteLogic>().initial_Destinations)
    //                {
    //                    Debug.Log("hey");
    //                    recursionObject = RecursionCheck(_destination, playerClaimedLocalRoutes);
    //                    return RecursionCheck(_destination, playerClaimedLocalRoutes);
    //                }
    //            }
    //        }
    //    }
    //    else if (destination.GetComponent<DestinationLogic>().end_destination)
    //    {
    //        Debug.Log("heheh");
    //        recursionObject = destination;
    //        return destination;
    //    }

    //    return recursionObject;
    //}

}
