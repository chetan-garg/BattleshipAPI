using Enumerations;
using InterfaceLibrary;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipAttacker
{
    public class BattleshipAttacker : IShipAttacker
    {
        ILogger _logger;
        public BattleshipAttacker(ILogger logger)
        {
            _logger = logger;
        }
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
