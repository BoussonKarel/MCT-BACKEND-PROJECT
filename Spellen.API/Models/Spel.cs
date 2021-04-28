using System;
using System.Collections.Generic;

namespace Spellen.API.Models
{
    public class Spel
    {
        public Guid SpelId { get; set; }
        public string Naam { get; set; }
        public string Uitleg { get; set; }
        public string Duur { get; set; } // "x tot x minuten"
        public List<string> Terrein { get; set; }
        public int Leeftijd_vanaf { get; set; }
        public int Leeftijd_tot { get; set; }
        public int Spelers_min { get; set; }
        public int Spelers_max { get; set; }
        public List<Categorie> Categorieen { get; set; }
        public List<Materiaal> Materiaal { get; set; }
    }
}
