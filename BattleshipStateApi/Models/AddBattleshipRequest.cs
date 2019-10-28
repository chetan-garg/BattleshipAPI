using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipStateApi.Models
{
    public class AddBattleshipRequest
    {
        [Required]
        [Range(1, 10)]
        public int XCoordinate { get; set; }
        [Required]
        [Range(1, 10)]
        public int YCoordinate { get; set; }
        [Required]
        public int Width { get; set; }
        [Required]
        public string Orientation { get; set; }
    }
}
