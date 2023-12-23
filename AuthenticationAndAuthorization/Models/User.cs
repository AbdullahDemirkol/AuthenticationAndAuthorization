namespace AuthenticationAndAuthorization.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Fullname { get { return $"{Firstname} {Surname}"; } }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
