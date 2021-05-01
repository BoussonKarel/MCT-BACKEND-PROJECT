using System;
using System.Text.Json.Serialization;

namespace Spellen.API.Models
{
    public class GameItem
    {
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
        [JsonIgnore]
        public Guid GameId { get; set; }
    }
}
