using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Threading;

public class ColourPileLogic : MonoBehaviour, IPointerClickHandler
{
    #region VARIABLES:
    [SerializeField] public string colour;
    [SerializeField] public int numberOfCards;
    [SerializeField] public TextMeshProUGUI numberDisplay;
    [SerializeField] public TextMeshProUGUI selectedDisplay;
    [SerializeField] public bool _colourSelected= false;


    [SerializeField] public GameObject[] p1_colourPiles;
    [SerializeField] public GameObject[] p2_colourPiles;
    #endregion

    private void Awake()
    {
        p1_colourPiles = GameObject.FindGameObjectsWithTag("p1_colourPile");
        p2_colourPiles = GameObject.FindGameObjectsWithTag("p2_colourPile");
    }
    private void Update()
    {
        numberOfCards = transform.childCount - 2; // number of children (-2 for the TMPro's)
        numberDisplay.text = numberOfCards.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _colourSelected = true;

        foreach (var p in p1_colourPiles)
        {
            p.GetComponent<ColourPileLogic>().selectedDisplay.gameObject.SetActive(false);
        }
        foreach (var p in p2_colourPiles)
        {
            p.GetComponent<ColourPileLogic>().selectedDisplay.gameObject.SetActive(false);
        }

        selectedDisplay.gameObject.SetActive(true);
    }
}
