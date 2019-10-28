using BattleShip.BattleshipApiEntities;
using Enumerations;
using InterfaceLibrary;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardGameCoordinator
{
    public class BattleshipCoordinator : IBoardCoordinator
    {
        ILogger _logger;
        IShipPlacementValidator _validator;
        IShipAttacker _attacker;
        public BattleshipCoordinator(ILogger logger, IShipPlacementValidator validator, IShipAttacker attacker)
        {
            _logger = logger;
            _validator = validator;
            _attacker = attacker;
        }
        public bool AddBattleship(IBoard board, IBoardCell startCell, IShip ship)
        {
            if (board == null || startCell == null || ship == null)
            {
                _logger.LogError("The required values to place the battleship are null.");
                return false;
            }

            var startingCell = board.BoardCells.Where(x => x.XCoordinate == startCell.XCoordinate && x.YCoordinate == startCell.YCoordinate).FirstOrDefault();
            if (startingCell == null)
            {
                _logger.LogError("The starting coordinates doesn't seem to be on the board.");
                return false;
            }

            if (_validator.ValidateShipCanBePlaced(ship, startCell, board))
            {
                _validator.ListOfCellsAffected(ship, startCell, board).ToList().ForEach(x => x.Occupied = true);
                return true;
            }

            return false;
        }

        public AttackResult Attack(IBoard board, IBoardCell cellToAttack)
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
            return _attacker.AttackShip(board, cellToAttack);
        }

        public IBoard CreateBoard()
        {
            return BattleshipBoard.CreateBoard();
        }

        #region Private Methods
        

        #endregion

    }
}
