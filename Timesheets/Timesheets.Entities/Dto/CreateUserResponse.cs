namespace Timesheets.Entities.Dto
{
    public sealed class CreateUserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; } 
        public int Age { get; set; }
        public string Login { get; set; }
    }
}
