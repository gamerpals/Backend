namespace GamerPalsBackend.DataObjects.Models
{
    public class Parameter
    {
        public int ParameterID { get; set; }
        public Game Game { get; set; }
        public string ParameterName { get; set; }
        public string ParameterType { get; set; }
    }
}
