using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCardManager : MonoBehaviour
{
    [SerializeField] List<TrainCard> trainCards = new List<TrainCard>();
    [SerializeField] TrainCard cs_card;
    [SerializeField] GameObject go_card;
    private int redTrains = 12;
    private int greenTrains = 12;
    private int blueTrains = 12;
    private int blackTrains = 12;
    private int whiteTrains = 12;
    private int pinkTrains = 12;
    private int orangeTrains = 12;
    private int yellowTrains = 12;
    private int locomotive = 12;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
