using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class TrainCard : MonoBehaviour
{
    [SerializeField] public string colour;
    [SerializeField] public bool isLocomotive;
    [SerializeField] public bool faceUp = true;
    [SerializeField] public bool cardSelected = false;

    #region OTHER SCRIPTS:
    [SerializeField] PlayerHand cs_playerHand;
    [SerializeField] GameObject go_playerhand;
    [SerializeField] MarketManager cs_marketManager;
    [SerializeField] GameObject go_marketManager;
    #endregion

    void Start()
    {
        go_playerhand = GameObject.Find("player1");
        cs_playerHand = go_playerhand.GetComponent<PlayerHand>();
        go_marketManager = GameObject.Find("TrainMarket");
        cs_marketManager = go_marketManager.GetComponent<MarketManager>();

        if (isLocomotive == true)
        {
            if (transform.IsChildOf(cs_marketManager.cardSlot1.transform))
            {
                cs_marketManager.locomotivesOnMarket += 1;
                Debug.Log("locomotiveOnMarket = " + cs_marketManager.locomotivesOnMarket);
            }
            else if (transform.IsChildOf(cs_marketManager.cardSlot2.transform))
            {
                cs_marketManager.locomotivesOnMarket += 1;
                Debug.Log("locomotiveOnMarket = " + cs_marketManager.locomotivesOnMarket);
            }
            else if (transform.IsChildOf(cs_marketManager.cardSlot3.transform))
            {
                cs_marketManager.locomotivesOnMarket += 1;
                Debug.Log("locomotiveOnMarket = " + cs_marketManager.locomotivesOnMarket);
            }
            else if (transform.IsChildOf(cs_marketManager.cardSlot4.transform))
            {
                cs_marketManager.locomotivesOnMarket += 1;
                Debug.Log("locomotiveOnMarket = " + cs_marketManager.locomotivesOnMarket);
            }
            else if (transform.IsChildOf(cs_marketManager.cardSlot5.transform))
            {
                cs_marketManager.locomotivesOnMarket += 1;
                Debug.Log("locomotiveOnMarket = " + cs_marketManager.locomotivesOnMarket);
            }
        }
        // Debug.Log("Card colour is: "+ colour);
        // Debug.Log("Card faceUP: " + faceUp);
    }
    private void LateUpdate()
    {
        if (isLocomotive == true)
        {
            if (transform.IsChildOf(cs_marketManager.cardSlot1.transform))
            {
                cs_marketManager.locomotivesOnMarket += 1;
                Debug.Log("locomotiveOnMarket = " + cs_marketManager.locomotivesOnMarket);
            }
            else if (transform.IsChildOf(cs_marketManager.cardSlot2.transform))
            {
                cs_marketManager.locomotivesOnMarket += 1;
                Debug.Log("locomotiveOnMarket = " + cs_marketManager.locomotivesOnMarket);
            }
            else if (transform.IsChildOf(cs_marketManager.cardSlot3.transform))
            {
                cs_marketManager.locomotivesOnMarket += 1;
                Debug.Log("locomotiveOnMarket = " + cs_marketManager.locomotivesOnMarket);
            }
            else if (transform.IsChildOf(cs_marketManager.cardSlot4.transform))
            {
                cs_marketManager.locomotivesOnMarket += 1;
                Debug.Log("locomotiveOnMarket = " + cs_marketManager.locomotivesOnMarket);
            }
            else if (transform.IsChildOf(cs_marketManager.cardSlot5.transform))
            {
                cs_marketManager.locomotivesOnMarket += 1;
                Debug.Log("locomotiveOnMarket = " + cs_marketManager.locomotivesOnMarket);
            }


        }

    }

    public void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            transform.parent = cs_playerHand.handSlot.transform;
            transform.position = cs_playerHand.handSlot.transform.position;
            cardSelected = true;

            if (isLocomotive == true)
            {
                cs_marketManager.locomotivesOnMarket -= 1;
                Debug.Log("locomotivesOnMarket - 1");
                Debug.Log("locomotivesOnMarket =" + cs_marketManager.locomotivesOnMarket);
            }


        }   
    }

}
