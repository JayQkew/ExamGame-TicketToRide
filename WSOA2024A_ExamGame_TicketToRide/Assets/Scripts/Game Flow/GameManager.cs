using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    [SerializeField] PlayerManager cs_player1Manager;
    [SerializeField] PlayerManager cs_player2Manager;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.MainMenu);
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
}

public enum GameState
{
    MainMenu,
    Player1Turn,
    Player2Turn,
    LastRound,
    Winner,
}