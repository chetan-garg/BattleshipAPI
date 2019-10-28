using InterfaceLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.BattleshipApiEntities
{

    /// <summary>
    /// 10x10 Battleship board.
    /// </summary>
    [Serializable]
    public class BattleshipBoard : IBoard
    {
        /// <summary>
        /// Atomic item of a board the cellular items.
        /// </summary>
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

        /// <summary>
        /// Creates the board for you.
        /// </summary>
        /// <returns>The newly created board.</returns>
        public static IBoard CreateBoard()
        {
            IBoard battleshipBoard = new BattleshipBoard();
            return battleshipBoard;
        }
    }
}
