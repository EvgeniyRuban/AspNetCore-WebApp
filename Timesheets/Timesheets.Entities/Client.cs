using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timesheets.Entities
{
    [Table("Clients", Schema = "Test")]
    public sealed class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
        public User User { get; set; }
    }
}
