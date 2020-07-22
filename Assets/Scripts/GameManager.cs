using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public delegate void SimpleEvent();
    public delegate void OnGameOver(Player winner);
    public event SimpleEvent onStartGameEvent;
    public event SimpleEvent onSwitchPlayerEvent;
    public event OnGameOver onGameOverEvent;

    Player[] players;
    int playerIndex = 0;
    Chip activeChip;
    bool useHighlight;

    IMovementRule currentMovementRule;
    List<Field> allowedFields = new List<Field>();

    private void Awake()
    {
        singleton = this;
    }

    public void SetMovementRule(IMovementRule movementRule)
    {
        currentMovementRule = movementRule;
    }

    public void SetHighlight(bool value)
    {
        useHighlight = value;
    }

    void InitializePlayers()
    {
        players = new Player[2] {
            new Player("White", Chip.ChipColor.white, 0),
            new Player("Black", Chip.ChipColor.black, 1) };
    }

    public void StartGame()
    {
        InitializePlayers();
        InputManager.singleton.enabled = true;
        CanvasManager.singleton.SetPlayerNames(players);

        onStartGameEvent?.Invoke();        
    }

    public void GameOver(Player winner)
    {
        InputManager.singleton.enabled = false;

        onGameOverEvent?.Invoke(winner);
    }

    public void SetActiveChip(Chip chip)
    {
        if (chip.chipColor != players[playerIndex].GetColor())
            return;

        ClearAllowedFieldsList();

        activeChip = chip;
        allowedFields = currentMovementRule.GetAllowedFields(chip.GetCurrentField().GetCoord());

        if (useHighlight)
            HighlightAllowedFields();
    }

    public void MoveActiveChip(Field field)
    {
        if (allowedFields.Contains(field)) {
            activeChip.SetCurrentField(field);
            ClearAllowedFieldsList();
            SwitchPlayer();
        }
    }

    public Player GetPlayer(int index)
    {
        return players[index];
    }

    public Player GetCurrentPlayer()
    {
        return players[playerIndex];
    }

    void SwitchPlayer()
    {
        GetCurrentPlayer().IncreaseTurnCount();
        CanvasManager.singleton.UpdatePlayerTurnCount();

        playerIndex = playerIndex == 0 ? 1 : 0;
        onSwitchPlayerEvent?.Invoke();
    }

    void ClearAllowedFieldsList()
    {
        foreach(Field field in allowedFields) {
            field.SwitchHighlight(false);
        }
        allowedFields.Clear();
    }

    void HighlightAllowedFields()
    {
        foreach (Field field in allowedFields) {
            field.SwitchHighlight(true);
        }
    }

}
