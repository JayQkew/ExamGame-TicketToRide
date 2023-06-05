using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColourPileLogic : MonoBehaviour
{
    #region VARIABLES:
    [SerializeField] public string colour;
    [SerializeField] public int numberOfCards;
    [SerializeField] public TextMeshProUGUI numberDisplay;
    #endregion
    private void Update()
    {
        numberOfCards = transform.childCount - 1; // number of children (-1 for the TMPro)
        numberDisplay.text = numberOfCards.ToString();
    }

}
