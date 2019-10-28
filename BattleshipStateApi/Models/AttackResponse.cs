using Enumerations;
using InterfaceLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipStateApi.Models
{
    public class AttackResponse
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public string Result { get; set; }
        public IBoard Board { get; set; } 
    }
}
