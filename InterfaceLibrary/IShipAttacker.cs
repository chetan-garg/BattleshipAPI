using Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceLibrary
{
    public interface IShipAttacker
    {
        AttackResult AttackShip(IBoard board, IBoardCell cellToAttack);
    }
}
