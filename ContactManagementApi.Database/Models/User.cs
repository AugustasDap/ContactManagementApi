namespace ContactManagementApi.Database.Models
{
    public class User : CommonProperties
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public UserRole Role { get; set; }
        public ICollection<Person> People { get; set; } = [];
        
    }
}