using System;

namespace MCT_BACKEND_PROJECT.Models
{
    public class VariCombi
    {
        public Guid VariCombiId { get; set; }
        public Guid SpelId1 { get; set; }
        public Guid SpelId2 { get; set; }
        public string Uitleg { get; set; }
    }
}
