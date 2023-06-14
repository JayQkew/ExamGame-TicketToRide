using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject Main;
    public GameObject Options;
    public GameObject Credits;

    public void Start()
    {
        Main.SetActive(true);
        Options.SetActive(false);
        Credits.SetActive(false);
    }

    public void OptionsBTN()
    {
        Main.SetActive(false);
        Options.SetActive(true);
        Credits.SetActive(false);
    }

    public void CreditsBTN()
    {
        Main.SetActive(false);
        Options.SetActive(false);
        Credits.SetActive(true);
    }

    public void OptionsExit()
    {
        Main.SetActive(true);
        Options.SetActive(false);
        Credits.SetActive(false);
    }
    public void CreditsExit()
    {
        Main.SetActive(true);
        Options.SetActive(false);
        Credits.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
   

}
