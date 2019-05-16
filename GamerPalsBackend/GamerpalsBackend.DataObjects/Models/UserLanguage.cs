namespace GamerPalsBackend.DataObjects.Models
{
    public class UserLanguage
    {
        public int UserID { get; set; }
        public int LanguageID { get; set; }
        public User User { get; set; }
        public Language Language { get; set; }
    }
}
