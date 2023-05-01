using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class TrainCard : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] public string colour;
    [SerializeField] public bool isLocomotive;
    [SerializeField] public bool faceUp = true;
    [SerializeField] TextMeshPro text;
    void Start()
    {
        text.text = colour;
        // Debug.Log("Card colour is: "+ colour);
        // Debug.Log("Card faceUP: " + faceUp);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointer down");
    }
}
