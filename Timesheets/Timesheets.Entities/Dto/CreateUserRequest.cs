namespace Timesheets.Entities.Dto
{
    public sealed class CreateUserRequest
    {
        public string UserName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
    }
}
