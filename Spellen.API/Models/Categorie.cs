using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Spellen.API.Models
{
    public class Categorie
    {
        public Guid CategorieId { get; set; }
        public string Naam { get; set; }
        [JsonIgnore]
        public List<Spel> Spellen { get; set; }
    }
}
