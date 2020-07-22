using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MovementRule2 : IMovementRule
{
    //фишка перепрыгивает по вертикали и горизонтали

    List<Field> allowedFields;

    bool[,] jumpDirections = { // матрица доступных направлений для перепрыгивания
        { false, true, false }, 
        { true, false, true }, 
        { false, true, false } };

    public List<Field> GetAllowedFields(Field.FieldCoord coord)
    {
        allowedFields = new List<Field>();

        for (int i = -1; i <= 1; i++) {
            for (int j = -1; j <= 1; j++) {

                if (i != 0 || j != 0) {
                    Field field = GetField(coord.x + i, coord.y + j);
                    ValidateField(field, i, j);
                }
            }
        }

        return allowedFields;
    }

    void ValidateField(Field field, int dirX, int dirY)
    {
        if (!field)
            return;

        if (!field.GetChip()) {
            allowedFields.Add(field);
        } else if (jumpDirections[dirX + 1, dirY + 1]) {
            ValidateFieldsAfterJump(field.GetCoord().x + dirX, field.GetCoord().y + dirY);
        }

    }

    void ValidateFieldsAfterJump(int x, int y)
    {
        Field field = GetField(x, y);
        if (!field || field.GetChip() || allowedFields.Contains(field))
            return;

        allowedFields.Add(field);

        for (int i = -1; i <= 1; i++) {
            for (int j = -1; j <= 1; j++) {
                if (jumpDirections[i + 1, j + 1]) {
                    Field nextField = GetField(x + i, y + j);
                    if (nextField && nextField.GetChip()) {
                        ValidateFieldsAfterJump(nextField.GetCoord().x + i, nextField.GetCoord().y + j);
                    }
                }
            }
        }

    }

    Field GetField(int x, int y)
    {
        return ChessboardManager.singleton.GetField(x, y);
    }

}
