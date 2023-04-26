using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        WAITING_TO_START, PLAYING, GAME_OVER
    }

    public static GameManager Instance { get; private set; }

    public event EventHandler OnGameStateChanged;

    [SerializeField] private MainCharacterController mainCharacterController;
    [SerializeField] private StructuresSpawner structuresSpawner;

    private GameState gameState;

    private void Awake()
    {
        Instance = this;

        gameState = GameState.WAITING_TO_START;

        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (gameState == GameState.WAITING_TO_START)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartTheGame();
            }
        }

        if (gameState == GameState.GAME_OVER)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ReStartTheGame();
            }
        }
    }

    private void StartTheGame()
    {
        gameState = GameState.PLAYING;

        Time.timeScale = 1f;

        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public void ReStartTheGame()
    {
        mainCharacterController.RestoreInitialState();
        structuresSpawner.DestroyAllChildren();

        gameState = GameState.WAITING_TO_START;

        OnGameStateChanged?.Invoke(this, EventArgs.Empty);

        Time.timeScale = 0f;
    }

    public void OverTheGame()
    {
        gameState = GameState.GAME_OVER;

        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }
}
