using InterfaceLibrary;
using System;

namespace BattleShip.BattleshipApiEntities
{
    [Serializable]
    public class BoardCell : IBoardCell
    {
        public bool Occupied { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public bool IsHit { get; set; }
    }
}
