using Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceLibrary
{
    public interface IShip
    {
        public int Width { get; set; }
        public OrientationType Orientation { get; set; }
    }
}
