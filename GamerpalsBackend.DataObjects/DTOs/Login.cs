namespace GamerPalsBackend.DataObjects.Models
{
    public class Login
    {
        public string Token { get; set; }
        public int Type { get; set; } // 0 Facebook/ 1 Google / 2 TestUser
    }
}
