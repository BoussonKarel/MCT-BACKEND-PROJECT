using System;
using System.ComponentModel.DataAnnotations;

namespace MCT_BACKEND_PROJECT.Models
{
    public class SpelMateriaal
    {
        // Samengestelde primaire sleutel (composite key) voor SpelMateriaal
        public Guid MateriaalId { get; set; }
        public Guid SpelId { get; set; }
        public int Aantal { get; set; }
    }
}
