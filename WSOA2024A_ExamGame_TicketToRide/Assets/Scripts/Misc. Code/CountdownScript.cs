using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownScript : MonoBehaviour
{
    public GameObject switchPlayerOverlay;
    public TextMeshProUGUI countDownTxt;
    public float timer = 5f;
    private bool countingDown = false;

    [SerializeField] GameManager cs_gameManager;

    private void Start()
    {
        switchPlayerOverlay.SetActive(false);
    }

    public void StartCountdown()
    {
        if(cs_gameManager.hasGameEnded == false)
        {
            if (!countingDown)
            {
                switchPlayerOverlay.SetActive(true);
                countingDown = true;
                countDownTxt.text = timer.ToString("F0");
                InvokeRepeating("UpdateTimer", 1f, 1f);
            }
        }
    }

    void UpdateTimer()
    {
        timer -= 1f;

        if (timer >= 0f)
        {
            countDownTxt.text = timer.ToString("F0");
        }
        else
        {
            ResetCountdown();
        }
    }

    void ResetCountdown()
    {
        countingDown = false;
        switchPlayerOverlay.SetActive(false);
        timer = 5f;
        CancelInvoke("UpdateTimer");
    }

    public void GameIsOver()
    {
        if(cs_gameManager.hasGameEnded == true)
        {
            switchPlayerOverlay.SetActive(false);
        }
    }
}
