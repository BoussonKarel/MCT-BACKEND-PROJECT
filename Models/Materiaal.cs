using System;
using System.Collections.Generic;

namespace MCT_BACKEND_PROJECT.Models
{
    public class Materiaal
    {
        public Guid MateriaalId { get; set; }
        public string Item { get; set; }
        public List<SpelMateriaal> Spellen { get; set; }
    }
}
