using System;

namespace Spellen.API.Models
{
    public class VariCombi
    {
        public Guid VariCombiId { get; set; }
        public Guid GameId1 { get; set; }
        public Guid GameId2 { get; set; }
        public string Explanation { get; set; }
    }
}
