using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> handSlots = new List<GameObject>();
    [SerializeField] public GameObject handSlot;    //test
    [SerializeField] public bool playerTurn;

    #region OTHER SCRIPTS:
    [SerializeField] UI_TrainDeckManager cs_trainDeckManager;
    [SerializeField] GameObject go_trainDeckManager;
    [SerializeField] UI_TrainCard cs_trainCard;
    [SerializeField] GameObject go_trainCard;
    #endregion

    private void Awake()
    {
        cs_trainCard = go_trainCard.GetComponent<UI_TrainCard>();
        cs_trainDeckManager = go_trainDeckManager.GetComponent<UI_TrainDeckManager>();
    }

    public void CreateHandSlot()
    {
        if (cs_trainCard == true)
        {
        }
    }

}
