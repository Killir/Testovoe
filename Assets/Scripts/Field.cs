using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField]
    GameObject highlight = null;

    FieldCoord coord;
    SpriteRenderer sprite;
    Chip chip = null;


    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void SetChip(Chip chip)
    {
        this.chip = chip;
    }

    public Chip GetChip()
    {
        return chip;
    }

    public void SetSpriteColor(Color color)
    {
        sprite.color = color;
    }

    public void SetCoord(int x, int y)
    {
        coord = new FieldCoord(x, y);
    }

    public FieldCoord GetCoord()
    {
        return coord;
    }

    public void SwitchHighlight(bool value)
    {
        highlight.SetActive(value);
    }

    public struct FieldCoord
    {
        public int x;
        public int y;

        public FieldCoord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

}
