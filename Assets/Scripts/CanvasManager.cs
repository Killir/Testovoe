using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager singleton;

    public GameObject rulesPanel;
    public GameObject winPanel;
    public GameObject HUD;
    public Text[] playerNameTexts;
    public Text[] playerTurnCountTexts;
    public Text currentPlayerNameText;
    public Text winnerNameText;
    public Text winnerTurnCountText;
    public Toggle highlightToggle;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        GameManager.singleton.onStartGameEvent += OnStartGame;
        GameManager.singleton.onSwitchPlayerEvent += UpdateCurrentPlayerName;
        GameManager.singleton.onGameOverEvent += ShowWinPanel;
    }

    public void SetPlayerNames(Player[] players)
    {
        for (int i = 0; i < playerNameTexts.Length; i++) {
            playerNameTexts[i].text = players[i].GetName();
        }
    }

    public void UpdateCurrentPlayerName()
    {
        currentPlayerNameText.text = "Твой ход, " + GameManager.singleton.GetCurrentPlayer().GetName();
    }

    public void UpdatePlayerTurnCount()
    {
        int index = GameManager.singleton.GetCurrentPlayer().GetIndex();
        int turnCount = GameManager.singleton.GetCurrentPlayer().GetTurnCount();
        playerTurnCountTexts[index].text = turnCount.ToString();
    }

    public void ResetPlayerTurnCountTexts()
    {
        foreach (Text text in playerTurnCountTexts) {
            text.text = "0";
        }
    }

    void OnStartGame()
    {
        rulesPanel.SetActive(false);
        ResetPlayerTurnCountTexts();
        UpdateCurrentPlayerName();
        HUD.SetActive(true);
    }

    void ShowWinPanel(Player player)
    {
        HUD.SetActive(false);

        winnerNameText.text = player.GetName();
        winnerTurnCountText.text = player.GetTurnCount().ToString();
        winPanel.SetActive(true);
    }

    public void RuleButtons(int index)
    {
        IMovementRule movementRule = null;
        switch (index) {
            case 1:
                movementRule = new MovementRule1();
                break;
            case 2:
                movementRule = new MovementRule2();
                break;
            case 3:
                movementRule = new MovementRule3();
                break;
        }

        GameManager.singleton.SetHighlight(highlightToggle.isOn);
        GameManager.singleton.SetMovementRule(movementRule);
        GameManager.singleton.StartGame();
    }

    public void RestartButton()
    {
        _SceneManager.LoadScene("Main");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
