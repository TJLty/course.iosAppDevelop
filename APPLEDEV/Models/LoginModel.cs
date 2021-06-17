namespace APPLEDEV.Models
{
    public class LoginRequest
    {

        public string Username{ get; set; }
        public string Password { get; set;}
    }

    public class RoomMate
    {
        //public string username;
        //public int status;
    }
    public class LoginResponse
    {
        public int Status{ get; set; }
        public string Token{ get; set; }
    }
}