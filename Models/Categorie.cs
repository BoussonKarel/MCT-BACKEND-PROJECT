using System;
using System.Collections.Generic;

namespace MCT_BACKEND_PROJECT.Models
{
    public class Categorie
    {
        public Guid CategorieId { get; set; }
        public string Naam { get; set; }
        public List<Spel> Spellen { get; set; }
    }
}
