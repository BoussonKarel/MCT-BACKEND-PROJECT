using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Spellen.API.Models
{
    public class Item
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Game> Games { get; set; }
    }
}
