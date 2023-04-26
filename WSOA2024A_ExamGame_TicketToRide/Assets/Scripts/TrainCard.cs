using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainCard : MonoBehaviour
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
}
