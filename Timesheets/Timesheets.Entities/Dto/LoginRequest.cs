using System.ComponentModel.DataAnnotations;

namespace Timesheets.Entities.Dto
{
    /// <summary>
    /// Provides authetication form for the user request.
    /// </summary>
    public sealed class LoginRequest
    {
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Login { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
