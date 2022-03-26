using System.ComponentModel.DataAnnotations;

namespace Timesheets.Entities.Dto
{
    /// <summary>
    /// Provides an User model for a client requesting the creation of this model.
    /// </summary>
    public sealed class CreateUserRequest
    {
        [Required]
        [MinLength(1)][MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)][MaxLength(100)]
        public string Surname { get; set; }

        [Required]
        [MinLength(1)][MaxLength(100)]
        public string Login { get; set; }

        [Required]
        [MinLength(4)][MaxLength(100)]
        public string Password { get; set; }
        public int? Age { get; set; }
    }
}
