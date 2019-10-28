using Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceLibrary
{
    public interface IBoardCoordinator
    {
        public IBoard CreateBoard();

        public bool AddBattleship(IBoard board, IBoardCell startCell, IShip ship);
        public AttackResult Attack(IBoard board, IBoardCell cellToAttack);
    }
}
