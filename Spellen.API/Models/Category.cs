using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Spellen.API.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<GameCategory> GameCategories { get; set; }
    }
}
