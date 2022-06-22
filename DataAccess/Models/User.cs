namespace DataAccess.Models
{
    /// <summary>
    /// This class represents the model of the User table record from the database
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Jid { get; set; }
    }
}
