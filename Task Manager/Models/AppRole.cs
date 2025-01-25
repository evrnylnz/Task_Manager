namespace Task_Manager.Models
{
    public class AppRole
    {
        public int Id { get; set; }

        public string Role { get; set; }

        public List<AppUser> AppUsers { get; set; }
    }
}
