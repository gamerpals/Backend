using System;

namespace GamerPalsBackend.DataObjects.Models
{
    public class GlobalParameters
    {
        public int GlobalParametersID { get; set; }
        public DateTime Birthday { get; set; }
        public bool? Gender { get; set; }
        public string Country { get; set; }
    }
}
