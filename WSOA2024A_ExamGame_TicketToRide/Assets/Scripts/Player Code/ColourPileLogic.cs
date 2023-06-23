using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ColourPileLogic : MonoBehaviour, IPointerClickHandler
{
    #region VARIABLES:
    [SerializeField] public string colour;
    [SerializeField] public int numberOfCards;
    [SerializeField] public TextMeshProUGUI numberDisplay;
    [SerializeField] public bool colourSelect = false;
    [SerializeField] public bool _colourSelected= false;
    #endregion
    private void Update()
    {
        numberOfCards = transform.childCount - 1; // number of children (-1 for the TMPro)
        numberDisplay.text = numberOfCards.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(colourSelect)
        {
            _colourSelected = true;
            
        }
    }
}
