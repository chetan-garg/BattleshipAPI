using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceLibrary
{
    public interface IShipPlacementValidator
    {
        bool ValidateShipCanBePlaced(IShip ship, IBoardCell startingCell, IBoard board);
        IEnumerable<IBoardCell> ListOfCellsAffected(IShip ship, IBoardCell startingCell, IBoard board);
    }
}
