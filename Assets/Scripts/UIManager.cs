using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject waitingToStartPanel;
    [SerializeField] private GameObject gameOverPanel;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        MainCharacterController.Instance.OnScored += MainCharacter_OnScored;
    }

    private void GameManager_OnGameStateChanged(object sender, System.EventArgs args)
    {
        if (GameManager.Instance.GetGameState() == GameManager.GameState.WAITING_TO_START)
        {
            waitingToStartPanel.SetActive(true);
        }
        else
        {
            waitingToStartPanel.SetActive(false);
        }

        if (GameManager.Instance.GetGameState() == GameManager.GameState.GAME_OVER)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            gameOverPanel.SetActive(false);
        }
    }

    private void MainCharacter_OnScored(object sender, MainCharacterController.OnScoredEventArgs args)
    {
        scoreText.text = "SCORE: " + args.score;
    }
}
