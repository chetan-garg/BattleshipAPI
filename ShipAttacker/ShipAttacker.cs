using Enumerations;
using InterfaceLibrary;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipAttacker
{
    /// <summary>
    /// Chetan Garg: 28th October 2019
    /// This class contains the logic to perform an attack on a battleship and return whether it is a hit or miss.
    /// </summary>
    public class BattleshipAttacker : IShipAttacker
    {
        ILogger _logger;
        public BattleshipAttacker(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Perform an attack on the supplied board and return the outcome.
        /// </summary>
        /// <param name="board">The current board on which the attack will be performed.</param>
        /// <param name="cellToAttack">The board position on which the attack will be performed.</param>
        /// <returns>Outcome of the attack. Whether it is a Hit or a Miss.</returns>
        public AttackResult AttackShip(IBoard board, IBoardCell cellToAttack)
        {
            if (board == null)
            {
                _logger.LogError("The board is null.");
                throw new ArgumentNullException("The board is null and the attack cannot be completed.");
            }
            if (cellToAttack == null || cellToAttack.XCoordinate <= 0 || cellToAttack.YCoordinate <= 0)
            {
                _logger.LogError("The attack cell is not provided.");
                throw new ArgumentNullException("The board is null and the attack cannot be completed.");
            }
            var attackedCell = board.BoardCells.Where(x => x.XCoordinate == cellToAttack.XCoordinate && x.YCoordinate == cellToAttack.YCoordinate).FirstOrDefault();
            if (attackedCell != null && attackedCell.Occupied)
            {
                attackedCell.IsHit = true;
                return AttackResult.Hit;
            }

            return AttackResult.Miss;
        }
    }
}
