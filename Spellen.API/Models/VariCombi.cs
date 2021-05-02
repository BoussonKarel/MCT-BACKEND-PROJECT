using System;
using System.Collections.Generic;

namespace Spellen.API.Models
{
    public class VariCombi
    {

        // VARI COMBIS ARE LEFT OUT FOR NOW BECAUSE OF TIME
        // THEY CAN BE ADDED IN A NEXT UPDATE, A V2 OF THE API
        public Guid VariCombiId { get; set; }
        public List<GameVariCombi> GameVariCombis { get; set; }
        public string Explanation { get; set; }
    }
}
