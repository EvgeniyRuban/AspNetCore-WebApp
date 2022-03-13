using System.ComponentModel.DataAnnotations.Schema;

namespace Timesheets.Entities
{
    [Table("Users", Schema = "Test")]
    public sealed class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; }
    }
}
