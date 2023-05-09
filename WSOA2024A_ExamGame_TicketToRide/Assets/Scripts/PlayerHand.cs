using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PlayerHand : MonoBehaviour
{
    [SerializeField] public List <GameObject> handSlots = new List<GameObject>();
    [SerializeField] public GameObject handSlot;    //test
    [SerializeField] public bool playerTurn;

    #region OTHER SCRIPTS:
    [SerializeField] TrainDeckManager cs_trainDeckManager;
    [SerializeField] GameObject go_trainDeckManager;
    [SerializeField] TrainCard cs_trainCard;
    [SerializeField] GameObject go_trainCard;
    #endregion

    private void Start()
    {
        cs_trainCard = go_trainCard.GetComponent<TrainCard>();
        cs_trainDeckManager = go_trainDeckManager.GetComponent<TrainDeckManager>();
    }

    public void CreateHandSlot()
    {
        if (cs_trainCard == true)
        {
        }
    }
}
