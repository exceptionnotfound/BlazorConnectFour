using System;

namespace BlazorConnectFour.Data
{
    public class Position
    {
        public Position(int col, int row)
        {
            Column = col;
            Row = row;
        }


        public int Column { get; set; }

        public int Row { get; set; }

        public bool IsValid => GameBoard.IsValidPosition(this);

        public Position GetOffset(int step, Directions direction)
        {
            switch (direction)
            {
                case Directions.Horizontal:
                    return new Position(Column + step, Row);

                case Directions.Vertical:
                    return new Position(Column, Row + step);

                case Directions.DiagonalUpRight:
                    return new Position(Column + step, Row + step);

                case Directions.DiagonalDownRight:
                    return new Position(Column + step, Row - step);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
