using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Spellen.API.Models
{
    public class Materiaal
    {
        public Guid MateriaalId { get; set; }
        public string Item { get; set; }
        [JsonIgnore]
        public List<Spel> Spellen { get; set; }
    }
}
