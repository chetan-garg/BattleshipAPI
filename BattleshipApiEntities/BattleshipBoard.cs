using InterfaceLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.BattleshipApiEntities
{
    [Serializable]
    public class BattleshipBoard : IBoard
    {
        public List<IBoardCell> BoardCells { get; set; }

        private BattleshipBoard()
        {
            BoardCells = new List<IBoardCell>();
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    BoardCells.Add(new BoardCell() { XCoordinate = i, YCoordinate = j, Occupied = false, IsHit = false });
                }
            }
        }

        public static IBoard CreateBoard()
        {
            IBoard battleshipBoard = new BattleshipBoard();
            return battleshipBoard;
        }
    }
}
