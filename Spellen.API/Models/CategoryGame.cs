using System;
using System.Text.Json.Serialization;

namespace Spellen.API.Models
{
    public class CategoryGame
    {
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
        [JsonIgnore]
        public Guid GameId { get; set; }
    }
}
