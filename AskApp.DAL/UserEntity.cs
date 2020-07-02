using AskApp.Cross_Cutting;

namespace AskApp.DAL
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; }
    }
}