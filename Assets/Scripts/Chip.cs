using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    public enum ChipColor { black, white };

    public ChipColor chipColor;

    Field currentField;

    public void SetCurrentField(Field field)
    {
        if (currentField) {
            currentField.SetChip(null);
        }
        currentField = field;
        currentField.SetChip(this);
        transform.position = field.transform.position - Vector3.forward * 0.01f;
    }

    public Field GetCurrentField()
    {
        return currentField;
    }
}
