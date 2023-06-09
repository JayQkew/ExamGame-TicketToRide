using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestinationDeck : MonoBehaviour, IPointerClickHandler
{
    
    #region LISTS:
    [SerializeField] List<DestinationCard> sO_destinationCards = new List<DestinationCard>(); //THe list of destination tickets for the game]
    [SerializeField] List<GameObject> destinationCards = new List<GameObject>();
    [SerializeField] List<GameObject> discardedDestinationCards = new List<GameObject>();
    #endregion

    #region DESTINATION CARDS:
    [SerializeField] GameObject BostonMiami;
    [SerializeField] GameObject CalgaryPhoenix;
    [SerializeField] GameObject CalgarySaltLakeCity;
    [SerializeField] GameObject ChicagoNewOrleans;
    [SerializeField] GameObject ChicagoSantaFe;
    [SerializeField] GameObject DallasNewYork;
    [SerializeField] GameObject DenverElPaso;
    [SerializeField] GameObject DenverPittsburgh;
    [SerializeField] GameObject DuluthElPaso;
    [SerializeField] GameObject DuluthHouston;
    [SerializeField] GameObject HelenaLosAngeles;
    [SerializeField] GameObject KansasCityHouston;
    [SerializeField] GameObject LosAngelesChicago;
    [SerializeField] GameObject LosAngeleseMiami;
    [SerializeField] GameObject LosAngelesNewYork;
    [SerializeField] GameObject MontrealAtlanta;
    [SerializeField] GameObject MontrealNewOrleans;
    [SerializeField] GameObject NewYorkAtlanta;
    [SerializeField] GameObject PortlandNashville;
    [SerializeField] GameObject PortlandPhoenix;
    [SerializeField] GameObject SanFranciscoAtlanta;
    [SerializeField] GameObject SaultStMarieNashville;
    [SerializeField] GameObject SaultStMarieOklahomaCity;
    [SerializeField] GameObject SeattleLosAngeles;
    [SerializeField] GameObject SeattleNewYork;
    [SerializeField] GameObject TorontoMiami;
    [SerializeField] GameObject VancouverMontreal;
    [SerializeField] GameObject VancouverSantaFe;
    [SerializeField] GameObject WinnipegHouston;
    [SerializeField] GameObject WinnipegLittleRock;
    #endregion

    #region VARIABLES:
    [SerializeField] GameObject destinationDeck;
    [SerializeField] public GameObject destinationDiscardedPile;
    [SerializeField] public int cardsChosen;
    [SerializeField] public bool canDrawDestinationCard;
    [SerializeField] public bool destinationCardAction;
    #endregion

    #region OTHER GAME OBJECTS:
    [SerializeField] public GameObject[] choices = new GameObject[3];
    [SerializeField] GameObject p_destinationChoices;
    [SerializeField] public GameObject doneButton;
    [SerializeField] GameObject go_playerManager1;
    [SerializeField] GameObject go_playerManager2;
    #endregion
    void Start()
    {
        #region ADDING DESTINATIONS:
        destinationCards.Add(BostonMiami);
        destinationCards.Add(CalgaryPhoenix);
        destinationCards.Add(CalgarySaltLakeCity);
        destinationCards.Add(ChicagoNewOrleans);
        destinationCards.Add(ChicagoSantaFe);
        destinationCards.Add(DallasNewYork);
        destinationCards.Add(DenverElPaso);
        destinationCards.Add(DenverPittsburgh);
        destinationCards.Add(DuluthElPaso);
        destinationCards.Add(DuluthHouston);
        destinationCards.Add(HelenaLosAngeles);
        destinationCards.Add(KansasCityHouston);
        destinationCards.Add(LosAngelesChicago);
        destinationCards.Add(LosAngeleseMiami);
        destinationCards.Add(LosAngelesNewYork);
        destinationCards.Add(MontrealAtlanta);
        destinationCards.Add(MontrealNewOrleans);
        destinationCards.Add(NewYorkAtlanta);
        destinationCards.Add(PortlandNashville);
        destinationCards.Add(PortlandPhoenix);
        destinationCards.Add(SanFranciscoAtlanta);
        destinationCards.Add(SaultStMarieNashville);
        destinationCards.Add(SaultStMarieOklahomaCity);
        destinationCards.Add(SeattleLosAngeles);
        destinationCards.Add(SeattleNewYork);
        destinationCards.Add(TorontoMiami);
        destinationCards.Add(VancouverMontreal);
        destinationCards.Add(VancouverSantaFe);
        destinationCards.Add(WinnipegHouston);
        destinationCards.Add(WinnipegLittleRock);
        #endregion

        canDrawDestinationCard = true;
        destinationCardAction = false;
    }

    public void DrawDestinationCards(Vector3 position, Transform parent) // method to use when drawing a random destination card
    {
        bool p1_wasDeactive = false;
        bool p2_wasDeactive = false;

        if (!go_playerManager1.activeSelf)
        {
            p1_wasDeactive = true;
            go_playerManager1.SetActive(true);
        }
        if (!go_playerManager2.activeSelf)
        {
            p2_wasDeactive = true;
            go_playerManager2.SetActive(true);
        }

        int randomNumber = Random.Range(0, destinationCards.Count - 1);
        Instantiate(destinationCards[randomNumber], position, Quaternion.identity, parent);
        destinationCards.RemoveAt(randomNumber);

        if (p1_wasDeactive)
        {
            go_playerManager1.SetActive(false);
            p1_wasDeactive = false;
            p2_wasDeactive = false;
        }
        else if (p2_wasDeactive)
        {
            go_playerManager2.SetActive(false);
            p1_wasDeactive = false;
            p2_wasDeactive = false;
        }

    }

    public void DiscardDestinationCard(GameObject card) // method to use when discarding a destination card
    {
        discardedDestinationCards.Add(card);
        card.transform.SetParent(destinationDiscardedPile.transform);
        card.SetActive(false);
    }

    public void ShuffleDiscardedDestination() // Shuffles the discarded destination cards back into the destinationCards list
    {
        if(destinationCards.Count <= 0)
        {
            for (int i = 0; i < discardedDestinationCards.Count; i++)
            {
                discardedDestinationCards[i].SetActive(true);
                destinationCards.Add(discardedDestinationCards[i]);
                if (destinationCards.Count == discardedDestinationCards.Count)
                {
                    discardedDestinationCards.Clear();
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData) // when the action is chosen
    {
        if(canDrawDestinationCard == true)
        {
            p_destinationChoices.gameObject.SetActive(true); // set the panel active
            for (int i = 0; i < 3; i++)
            {
                DrawDestinationCards(choices[i].transform.position, choices[i].transform); // draw 3 random destination cards and parent them to the choices
            }
        }
    }

    public void DoneButtonClick()
    {
        foreach (GameObject choice in choices) // goes through each choice 
        {
            if (choice.transform.childCount == 1) // if any of them have children
            {
                DiscardDestinationCard(choice.transform.GetChild(0).gameObject); // discard those children
            }
        }
        p_destinationChoices.SetActive(false); // set the panel to false
        doneButton.gameObject.SetActive(false); // set the button itself to false

        destinationCardAction = true;
    }
}
