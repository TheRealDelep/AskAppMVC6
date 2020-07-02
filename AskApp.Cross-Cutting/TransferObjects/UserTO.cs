namespace AskApp.Cross_Cutting.TransferObjects
{
    public class UserTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; }
    }
}