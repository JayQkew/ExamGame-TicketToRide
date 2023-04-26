using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TrainDeckManager : MonoBehaviour
{
    [SerializeField] List<GameObject> trainCards = new List<GameObject>();

    #region TRAIN INT VARIABLES:
    private int redTrains = 12;
    private int greenTrains = 12;
    private int blueTrains = 12;
    private int blackTrains = 12;
    private int whiteTrains = 12;
    private int pinkTrains = 12;
    private int orangeTrains = 12;
    private int yellowTrains = 12;
    private int locomotive = 14;
    #endregion

    #region TRAIN GAME OBJECTS VARIABLES:
    [SerializeField] GameObject redCard;
    [SerializeField] GameObject greenCard;
    [SerializeField] GameObject blueCard;
    [SerializeField] GameObject blackCard;
    [SerializeField] GameObject whiteCard;
    [SerializeField] GameObject pinkCard;
    [SerializeField] GameObject orangeCard;
    [SerializeField] GameObject yellowCard;
    [SerializeField] GameObject locomotiveCard;
    #endregion

    void Start()
    {
        AddingCards(redTrains, redCard);
        AddingCards(greenTrains, greenCard);
        AddingCards(blueTrains, blueCard);
        AddingCards(blackTrains, blackCard);
        AddingCards(whiteTrains, whiteCard);
        AddingCards(pinkTrains, pinkCard);
        AddingCards(orangeTrains, orangeCard);
        AddingCards(yellowTrains, yellowCard);
        AddingCards(locomotive, locomotiveCard);
    }

    private void AddingCards(int trainType, GameObject cardType)
    {
        for (int i = 0;i < trainType; i++)
        {
            trainCards.Add (cardType);
        }
    }

    public void DrawCard()
    {
        int randomNumber = Random.Range(0, trainCards.Count - 1);
        Instantiate(trainCards[randomNumber], Vector2.zero, Quaternion.identity);
        trainCards.RemoveAt(randomNumber);
        Debug.Log("Cards left in Train Deck: " + trainCards.Count);
    }

}
