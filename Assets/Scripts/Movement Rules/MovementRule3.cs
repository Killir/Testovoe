using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRule3 : IMovementRule
{
    //фишка не может перепрыгивать, а делает только один шаг в любом направлении

    public List<Field> GetAllowedFields(Field.FieldCoord coord)
    {
        List<Field> allowedFields = new List<Field>();

        for (int i = -1; i <= 1; i++) {
            for (int j = -1; j <= 1; j++) {
                if (i != 0 || j != 0) {
                    Field field = ChessboardManager.singleton.GetField(coord.x + i, coord.y + j);
                    if (field && field.GetChip() == null) {
                        allowedFields.Add(field);
                    }
                }
            }
        }

        return allowedFields;
    }

}
