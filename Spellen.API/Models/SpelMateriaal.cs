using System;

namespace Spellen.API.Models
{
    public class SpelMateriaal
    {
        // Samengestelde primaire sleutel (composite key) voor SpelMateriaal
        public Guid MateriaalId { get; set; }
        public Guid SpelId { get; set; }
        public int Aantal { get; set; }
    }
}
