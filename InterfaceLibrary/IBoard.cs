using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceLibrary
{
    public interface IBoard
    {
        public List<IBoardCell> BoardCells { get; set; }
    }
}
