using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    string name;
    Chip.ChipColor color;
    int turnCount;
    int index;

    public Player(string name, Chip.ChipColor color, int index)
    {
        this.name = name;
        this.color = color;
        this.index = index;
        turnCount = 0;
    }

    public int GetIndex()
    {
        return index;
    }

    public string GetName()
    {
        return name;
    }

    public Chip.ChipColor GetColor()
    {
        return color;
    }

    public void IncreaseTurnCount()
    {
        turnCount++;
    }

    public int GetTurnCount()
    {
        return turnCount;
    }
}
