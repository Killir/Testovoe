using System.Collections.Generic;

public interface IMovementRule
{
    List<Field> GetAllowedFields(Field.FieldCoord coord);
}
