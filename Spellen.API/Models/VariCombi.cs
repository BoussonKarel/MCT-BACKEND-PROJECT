using System;
using System.Collections.Generic;

namespace Spellen.API.Models
{
    public class VariCombi
    {
        public Guid VariCombiId { get; set; }
        public List<GameVariCombi> GameVariCombis { get; set; }
        public string Explanation { get; set; }
    }
}
