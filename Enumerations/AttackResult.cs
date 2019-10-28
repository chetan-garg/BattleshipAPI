using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enumerations
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AttackResult
    {
        Hit,
        Miss
    }
}
