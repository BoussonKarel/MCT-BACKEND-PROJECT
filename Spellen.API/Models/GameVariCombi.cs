using System;
using System.Text.Json.Serialization;

namespace Spellen.API.Models
{
    public class GameVariCombi
    {
        public Guid VariCombiId { get; set; }
        public VariCombi VariCombi { get; set; }
        [JsonIgnore]
        public Guid GameId { get; set; }
    }
}
