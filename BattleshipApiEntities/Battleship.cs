using Enumerations;
using InterfaceLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.BattleshipApiEntities
{
    public class Battleship : IShip
    {
        public int Width { get; set; }
        public OrientationType Orientation { get; set; }
    }
}
