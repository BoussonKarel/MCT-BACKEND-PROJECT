using System;
using System.Collections.Generic;

namespace Spellen.API.Models
{
    public class Categorie
    {
        public Guid CategorieId { get; set; }
        public string Naam { get; set; }
        public List<Spel> Spellen { get; set; }
    }
}
