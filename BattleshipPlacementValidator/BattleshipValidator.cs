using InterfaceLibrary;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipPlacementValidator
{
    public class BattleshipValidator : IShipPlacementValidator
    {
        ILogger _logger;
        public BattleshipValidator(ILogger logger)
        {
            _logger = logger;
        }
        public bool ValidateShipCanBePlaced(IShip ship, IBoardCell startingCell, IBoard board)
        {
            if (ship.Width <= 0)
            {
                _logger.LogError("The ship's width cannot be less than 1.");
                return false;
            }

            if (ship.Orientation == Enumerations.OrientationType.Vertical &&
                (ship.Width + startingCell.XCoordinate) > board.BoardCells.Max(x => x.XCoordinate))
            {
                return false;
            }

            if (ship.Orientation == Enumerations.OrientationType.Horizontal &&
                (ship.Width + startingCell.YCoordinate) > board.BoardCells.Max(x => x.YCoordinate))
            {
                return false;
            }
            var listOfCellsAffected = ListOfCellsAffected(ship, startingCell, board);

            return !listOfCellsAffected.Any(x => x.Occupied);
        }

        public IEnumerable<IBoardCell> ListOfCellsAffected(IShip ship, IBoardCell startingCell, IBoard board)
        {
            switch (ship.Orientation)
            {
                case Enumerations.OrientationType.Vertical:
                    int numberOfCells = startingCell.XCoordinate + ship.Width;
                    return board.BoardCells.Where(x => x.XCoordinate >= startingCell.XCoordinate &&
                                                        x.YCoordinate >= startingCell.YCoordinate &&
                                                        x.XCoordinate < numberOfCells &&
                                                        x.YCoordinate <= startingCell.YCoordinate);
                case Enumerations.OrientationType.Horizontal:
                    numberOfCells = startingCell.YCoordinate + ship.Width;
                    return board.BoardCells.Where(x => x.XCoordinate >= startingCell.XCoordinate &&
                                                        x.YCoordinate >= startingCell.YCoordinate &&
                                                        x.XCoordinate <= startingCell.XCoordinate &&
                                                        x.YCoordinate < numberOfCells);
            }
            return null;
        }
    }
}
