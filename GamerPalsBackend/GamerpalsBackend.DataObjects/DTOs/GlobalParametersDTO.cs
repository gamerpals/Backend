using System.Collections.Generic;

namespace GamerPalsBackend.DataObjects.Models.DTOs
{
    public class GlobalParametersDTO
    {
        public GlobalParameters GlobalParameters { get; set; }
        public List<Language> Languages { get; set; }
    }
}
