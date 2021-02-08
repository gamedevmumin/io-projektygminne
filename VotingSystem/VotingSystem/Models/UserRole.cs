namespace VotingSystem.Models
{
    public class UserRole
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public static UserRole WithName(string name) => new UserRole { Name = name };
        private UserRole() { }
    }
}