namespace APPLEDEV.Models
{
    public class RegisterRequest
    {

        public string Username{ get; set; }
        public string Password { get; set;}
    }
    public class RegisterResponse
    {
        public int Status{ get; set; }
    }
}