using InterfaceLibrary;
using System;

namespace BattleShip.BattleshipApiEntities
{

    /// <summary>
    /// Atomic item of a board the cellular items.
    /// </summary>
    [Serializable]
    public class BoardCell : IBoardCell
    {
        public bool Occupied { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public bool IsHit { get; set; }
    }
}
