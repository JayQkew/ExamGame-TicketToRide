using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownScript : MonoBehaviour
{
    public GameObject switchPlayerOverlay;

    [SerializeField] GameManager cs_gameManager;

    private void Start()
    {
        switchPlayerOverlay.SetActive(false);
    }

    public void OpenSwitchingPanel()
    {
        if(cs_gameManager.hasGameEnded == false)
        {
            switchPlayerOverlay.SetActive(true);
        }
    }

    public void CloseSwitchingPanel()
    {
        if(cs_gameManager.hasGameEnded == false)
        {
            switchPlayerOverlay.SetActive(false);
        }
    }

    public void GameIsOver()
    {
        if(cs_gameManager.hasGameEnded == true)
        {
            switchPlayerOverlay.SetActive(false);
        }
    }
}
