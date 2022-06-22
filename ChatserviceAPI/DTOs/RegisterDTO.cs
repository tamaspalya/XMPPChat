namespace ChatserviceAPI.DTOs
{
    public class RegisterDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } 
        public string PhoneNumber { get; set; }
        public string Jid { get; set; }
        public string Address { get; set; }

    }
}
