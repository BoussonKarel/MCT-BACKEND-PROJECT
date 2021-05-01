using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Spellen.API.Models
{
    public class Game
    {
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public string Explanation { get; set; }
        public string Duration { get; set; } // "x tot x minuten"
        public List<string> Terrain { get; set; }
        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
        public int PlayersMin { get; set; }
        public int PlayersMax { get; set; }
        [JsonPropertyName("categories")]
        public List<GameCategory> GameCategories { get; set; }
        [JsonPropertyName("items")]
        public List<GameItem> GameItems { get; set; }
        [JsonIgnore]
        [JsonPropertyName("varicombis")]
        public List<GameVariCombi> GameVaricombis { get; set; }
    }
}
