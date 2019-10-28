using BattleShip.BattleshipApiEntities;
using Enumerations;
using InterfaceLibrary;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardGameCoordinator
{
    /// <summary>
    /// Chetan Garg: 28 Oct 2019
    /// Processing class that performs all the operations related to the game board.
    /// </summary>
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
        
        /// <summary>
        /// Add a battleship to the board at a specified position.
        /// </summary>
        /// <param name="board">Current board.</param>
        /// <param name="startCell">Starting position where the battleship has to be placed.</param>
        /// <param name="ship">The battleship that needs to be placed.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Perform a specified attack on the board.
        /// </summary>
        /// <param name="board">Current Board.</param>
        /// <param name="cellToAttack">Cell information that needs to be attacked.</param>
        /// <returns>Returns the output of an attack.</returns>
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

        /// <summary>
        /// Creates a 10x10 board,
        /// </summary>
        /// <returns>The blank board.</returns>
        public IBoard CreateBoard()
        {
            return BattleshipBoard.CreateBoard();
        }

        #region Private Methods
        

        #endregion

    }
}
