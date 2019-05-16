using System.Collections.Generic;

namespace GamerPalsBackend.DataObjects.Models
{
    public class Language
    {
        public int LanguageID { get; set; }
        public string LangShort { get; set; }
        public string LangLong { get; set; }
        public List<UserLanguage> Users { get; set; }
    }
}
