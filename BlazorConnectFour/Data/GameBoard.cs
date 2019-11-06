using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorConnectFour.Data
{
    public class GameBoard
    {
        public GamePiece[,] Board { get; set; }

        const int MAX_COL = 7;
        const int MAX_ROW = 6;

        public GameBoard()
        {
            Board = new GamePiece[MAX_COL, MAX_ROW];

            //Populate the Board with blank pieces
            for (int i = 0; i < MAX_COL; i++)
            {
                for (int j = 0; j < MAX_ROW; j++)
                {
                    Board[i, j] = new GamePiece(PieceColor.Blank);
                }
            }
        }

        /// <summary>
        /// Get next blank piece (if any)
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public Position GetNextBlank(int column)
        {
            // rows at top are 0, last row is 5
            for (int row = MAX_ROW - 1; row >= 0; row--)
            {
                var piece = Board[column, row];
                if (piece.Color == PieceColor.Blank)
                    return new Position(column, row);
            }
            return null; // column is full
        }

        /// <summary>
        /// return cells that make a row of 4, otherwise null
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public List<Position> GetWinningMoveCells(Position start)
        {
            const int WIN = 4;

            // get colour of counter at last move
            var colour = Board[start.Column, start.Row].Color;

            foreach (var direction in GetDirections())
            {
                // number of cells the same colour - start at 1 for last move
                var cells = GetMatchingCells(start, direction, colour);
                Console.WriteLine($"{colour}: {direction} = {cells.Count}");
                if (cells.Count == WIN) return cells;
            }

            return null;
        }

        private Directions[] GetDirections() => (Directions[])Enum.GetValues(typeof(Directions));


        /// <summary>
        /// Returns all matching cells in both directions
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="direction"></param>
        /// <param name="colour"></param>
        /// <returns>List of cell positions with same colour</returns>
        /// <remarks>
        /// I returned a list of matching cells so the program could perhaps
        /// highlight the winning row
        /// </remarks>
        private List<Position> GetMatchingCells(Position start, Directions direction, PieceColor colour)
        {
            // create a list of matching cells
            var result = new List<Position>() { start };

            // go in first in one direction, then other
            foreach (var factor in new int[] { 1, -1 })
            {
                for (int step = 1; step <= 3; step++)
                {
                    var cell = start.GetOffset(step * factor, direction);
                    // if invalid (off the board) or not a matching cell, exit loop
                    if (!cell.IsValid || GetPiece(cell) != colour)
                        break;
                    result.Add(cell);
                }
            }

            return result;
        }

        /// <summary>
        /// Get colour of the piece at this position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private PieceColor GetPiece(Position position)
        {
            // bounds check
            if (!position.IsValid) return PieceColor.Blank;
            return Board[position.Column, position.Row].Color;
        }

        /// <summary>
        /// Determine if a position is valid
        /// </summary>
        /// <param name="position">position to check</param>
        /// <returns></returns>
        public static bool IsValidPosition(Position position) =>
            position.Column >= 0 && position.Column < MAX_COL
            && position.Row >= 0 && position.Row < MAX_ROW;

    }

    public enum Directions { Horizontal, Vertical, DiagonalUpRight, DiagonalDownRight }



}
