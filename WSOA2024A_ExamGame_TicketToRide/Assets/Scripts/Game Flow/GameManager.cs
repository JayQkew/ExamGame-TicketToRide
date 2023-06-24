using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region GAME MANAGER STUFF
    [Header("Game Manager Shit")]
    public static GameManager Instance;
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;
    #endregion

    #region OTHER VARIABLES
    [Header("Other Variables")]
    [SerializeField] public GameObject endP1TurnButton;
    [SerializeField] public GameObject endP2TurnButton;
    [Header("Pause Game Stuff")]
    [SerializeField] GameObject PauseGamePanel;
    [SerializeField]private bool isPaused = false;
    [Header("Last Round BULLSHIIIIIIIIT")]
    [SerializeField] public bool hasLastRoundStarted;
    [SerializeField] public GameObject winScreen;
    [SerializeField] public bool hasGameEnded;
    #endregion

    #region OTHER SCRIPTS
    [Header("Other Scripts")]
    [SerializeField] PlayerManager cs_player1Manager;
    [SerializeField] PlayerManager cs_player2Manager;
    [SerializeField] CountdownScript cs_countdownScript;
    [SerializeField] WinScreenCode cs_winScreenCode;
    #endregion

    #region ROUND CODE
    [Header("Round Code")]
    [SerializeField] public TextMeshProUGUI roundNumberTxt;
    [SerializeField] public int currentTurn;
    [HideInInspector][SerializeField] public int currentRound;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.Player1Turn);
        hasGameEnded = false;
    }

    private void Update()
    {
        CheckSwitching();
        UpdateRoundNumber();
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.Player1Turn:
                HandlePlayer1Turn();
                break;
            case GameState.SwitchingPlayers:
                HandleSwitchingPlayers();
                break;
            case GameState.Player2Turn:
                HandlePlayer2Turn();
                break;
            case GameState.LastRound:
                HandleLastRound();
                break;
            case GameState.Winner:
                HandleWinner();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleMainMenu()
    {
        Debug.Log("Main Menu");
    }

    private void HandlePlayer1Turn()
    {
        Debug.Log("Player 1 turn");
    }

    private void HandleSwitchingPlayers()
    {
        Debug.Log("Switching between Players");
    }

    private void HandlePlayer2Turn()
    {
        Debug.Log("Player 2 Turn");
    }

    private void HandleLastRound()
    {
        Debug.Log("Last Round");
    }

    private void HandleWinner()
    {
        Debug.Log("Winner");
    }

    public void CheckSwitching()
    {
        if (cs_countdownScript.switchPlayerOverlay.activeSelf == true)
        {
            UpdateGameState(GameState.SwitchingPlayers);
        }
        else
        {
            if(hasLastRoundStarted == false)
            {
                if (endP1TurnButton.activeSelf == true)
                {
                    UpdateGameState(GameState.Player1Turn);
                }
                if (endP2TurnButton.activeSelf == true)
                {
                    UpdateGameState(GameState.Player2Turn);
                }
            }
            else if (hasLastRoundStarted == true)
            {
                UpdateGameState(GameState.LastRound);
            }
            
        }
    }

    public void CheckLastRound()
    {
        if(cs_player1Manager.trainPieces <= 2 || cs_player2Manager.trainPieces <= 2)
        {
            hasLastRoundStarted = true;

            if(hasLastRoundStarted == true)
            {
                UpdateGameState(GameState.LastRound);
            }
        }

        if(hasLastRoundStarted == true)
        {
            int previousRound = currentRound;
            currentRound = currentTurn / 2;

            if (currentRound == previousRound + 1)
            {
                Debug.Log("Final round has started");
                if (currentRound == previousRound + 2)
                {
                    hasGameEnded = true;
                    cs_winScreenCode.WinScreen.SetActive(true);
                }
            }
        }

    }

    public void UpdateRoundNumber()
    {
        int currentRound = currentTurn / 2;
        roundNumberTxt.text = currentRound.ToString();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game by setting the time scale to 0
        PauseGamePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game by setting the time scale back to 1
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

public enum GameState
{
    MainMenu,
    Player1Turn,
    SwitchingPlayers,
    Player2Turn,
    LastRound,
    Winner,
}