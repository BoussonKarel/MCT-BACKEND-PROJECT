using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Spellen.API.DTO
{
    public class GameDTO
    {
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public string Explanation { get; set; }
        public string Duration { get; set; } // "x tot x minuten"
        public List<string> Terrain { get; set; }
        [JsonPropertyName("age_from")]
        public int AgeFrom { get; set; }
        [JsonPropertyName("age_to")]
        public int AgeTo { get; set; }
        [JsonPropertyName("players_min")]
        public int PlayersMin { get; set; }
        [JsonPropertyName("players_max")]
        public int PlayersMax { get; set; }
        public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
        public List<ItemDTO> Items { get; set; } = new List<ItemDTO>();
        public List<VariCombiDTO> VariCombis { get; set; } = new List<VariCombiDTO>();
    }
}
