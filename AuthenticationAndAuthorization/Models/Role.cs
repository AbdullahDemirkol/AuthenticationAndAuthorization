namespace AuthenticationAndAuthorization.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
