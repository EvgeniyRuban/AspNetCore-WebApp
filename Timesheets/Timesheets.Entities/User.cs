using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timesheets.Entities
{
    [Table("Users", Schema = "Test")]
    public sealed class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(1)][MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)][MaxLength(100)]
        public string Surname { get; set; }
        public int? Age { get; set; }

        [Required]
        [MinLength(1)][MaxLength(100)]
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; }
    }
}
