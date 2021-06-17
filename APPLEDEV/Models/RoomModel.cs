using System.Dynamic;

namespace APPLEDEV.Models
{
    
    public class Room
    {
        public string Name { get; set; }
        public string Owner { get; set; }
    }

    public class AddRoomResponse
    {
        public int Status { set; get; }
    }
    
}