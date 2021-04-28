using System;
using System.Collections.Generic;

namespace Spellen.API.Models
{
    public class Materiaal
    {
        public Guid MateriaalId { get; set; }
        public string Item { get; set; }
        public List<Spel> Spellen { get; set; }
    }
}
