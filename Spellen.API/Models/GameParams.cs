using System;

namespace Spellen.API.Models
{
    public class GameParams
    {
        public string SearchQuery { get; set; } = null;
        public int? AgeFrom { get; set; } = null;
        public int? AgeTo { get; set; } = null;
        public int? PlayersMin { get; set; } = null;
        public int? PlayersMax { get; set; } = null;
        public Guid? CategoryId { get; set; } = null;
    }
}
