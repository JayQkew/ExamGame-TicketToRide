using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] GameObject handSlot;
    [SerializeField] public bool playerTurn;

    #region OTHER SCRIPTS:
    [SerializeField] TrainDeckManager cs_trainDeckManager;
    [SerializeField] GameObject go_trainDeckManager;
    [SerializeField] TrainCard cs_trainCard;
    [SerializeField] GameObject go_trainCard;
    #endregion


}
