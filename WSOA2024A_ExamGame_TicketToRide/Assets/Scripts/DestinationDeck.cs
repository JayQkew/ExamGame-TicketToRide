using System.Collections.Generic;
using UnityEngine;

public class DestinationDeck : MonoBehaviour
{
    //THe list of destination tickets for the game
    List<DestinationCard> destinationCards = new List<DestinationCard>();

    void Start()
    {
        //This foreach loop looks for all the objects in the DestinationCards folder and adds them to the list
        object[] loadedObjects = Resources.LoadAll("DestinationCards", typeof(DestinationCard));
        foreach (object obj in loadedObjects)
        {
            DestinationCard destinationCard = (DestinationCard)obj;
            destinationCards.Add(destinationCard);
        }
    }

    void Update()
    {

    }
}
