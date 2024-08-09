namespace Events.WebApi.Models.User
{
    public class DeleteUserDto
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
    }
}
