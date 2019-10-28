using Enumerations;
using InterfaceLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.BattleshipApiEntities
{
    /// <summary>
    /// The Battleship that can be placed on the board.
    /// </summary>
    public class Battleship : IShip
    {
        /// <summary>
        /// Width of the battleship.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Whether the battleship need to be placed horizontally or vertically.
        /// </summary>
        public OrientationType Orientation { get; set; }
    }
}
