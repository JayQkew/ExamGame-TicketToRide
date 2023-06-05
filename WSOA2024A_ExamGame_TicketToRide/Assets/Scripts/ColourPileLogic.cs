using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColourPileLogic : MonoBehaviour
{
    [SerializeField] public int numberOfCards;
    [SerializeField] public TextMeshProUGUI numberDisplay;
    private void Update()
    {
        numberOfCards = transform.childCount - 1; // number of children (-1 for the TMPro)
        numberDisplay.text = numberOfCards.ToString();
    }

}
