using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChessboardManager : MonoBehaviour
{
    public static ChessboardManager singleton;

    Field[,] fields = new Field[8, 8];

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        FillFieldsArray();
        GameManager.singleton.onStartGameEvent += GenerateChips;
        GameManager.singleton.onSwitchPlayerEvent += CheckFowWin;
    }

    public Field GetField(int i, int j)
    {
        if (i >= 0 && i < 8 && j >= 0 && j < 8)
            return fields[i, j];
        else 
            return null;
    }

    void FillFieldsArray()
    {
        Field[] temp = FindObjectsOfType<Field>();

        foreach (Field field in temp)
        {
            int x = Mathf.RoundToInt(field.transform.localPosition.x);
            int y = -Mathf.RoundToInt(field.transform.localPosition.y);

            field.SetCoord(x, y);
            fields[x, y] = field;
        }
    }

    public void GenerateChips()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject whiteChip = CreateChip("White chip");
                Chip whiteChipComp = whiteChip.GetComponent<Chip>();
                whiteChipComp.SetCurrentField(fields[0 + i, 7 - j]);
                fields[0 + i, 7 - j].SetChip(whiteChipComp);

                GameObject blackChip = CreateChip("Black chip");
                Chip blackChipComp = blackChip.GetComponent<Chip>();
                blackChipComp.SetCurrentField(fields[7 - i, 0 + j]);
                fields[7 - i, 0 + j].SetChip(blackChipComp);
            } 
        }
    }

    GameObject CreateChip(string name)
    {
        return Instantiate(ResourcesLoader.LoadChip(name));
    }

    public void CheckFowWin()
    {
        bool whiteWin = true;
        bool blackWin = true;

        for (int i = 5; i < 8; i++) {
            for (int j = 0; j < 3; j++) {
                Chip chip = fields[i, j].GetChip();
                whiteWin &= chip && chip.chipColor == Chip.ChipColor.white;
            }
        }

        for (int i = 0; i < 3; i++) {
            for (int j = 5; j < 8; j++) {
                Chip chip = fields[i, j].GetChip();
                blackWin &= chip && chip.chipColor == Chip.ChipColor.black;
            }
        }

        if (whiteWin)
            GameManager.singleton.GameOver(GameManager.singleton.GetPlayer(0));
        if (blackWin)
            GameManager.singleton.GameOver(GameManager.singleton.GetPlayer(1));
    }

}
