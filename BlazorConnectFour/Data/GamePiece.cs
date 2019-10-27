using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorConnectFour.Data
{
    public class GamePiece
    {
        public PieceColor Color;

        public GamePiece()
        {
            Color = PieceColor.Blank;
        }

        public GamePiece(PieceColor color)
        {
            Color = color;
        }
    }
}
