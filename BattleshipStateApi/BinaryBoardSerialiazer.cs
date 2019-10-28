using InterfaceLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace BattleshipStateApi
{
    public class BinaryBoardSerialiazer
    {
        public static byte[] SerializeObject(IBoard board)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, board);
                return ms.ToArray();
            }
        }

        public static IBoard DeSerializeObject(byte[] boardArray)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(boardArray))
            {
                return (IBoard)formatter.Deserialize(ms);
            }
        }
    }
}
