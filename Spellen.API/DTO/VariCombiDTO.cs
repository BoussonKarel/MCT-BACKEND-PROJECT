using System;
using System.Collections.Generic;

namespace Spellen.API.DTO
{
    public class VariCombiDTO
    {
        public Guid VariCombiId { get; set; }
        public List<GameDTO> Games { get; set; }
        public string Explanation { get; set; }
    }
}
