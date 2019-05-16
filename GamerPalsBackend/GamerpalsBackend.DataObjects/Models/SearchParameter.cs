namespace GamerPalsBackend.DataObjects.Models
{
    public class SearchParameter
    {
        public int ParameterID { get; set; }
        public int ActiveSearchID { get; set; }
        public Parameter Parameter { get; set; }
        public ActiveSearch ActiveSearch { get; set; }
        public string ParameterValue { get; set; }
    }
}
