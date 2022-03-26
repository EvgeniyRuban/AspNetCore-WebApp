using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timesheets.Entities
{
    [Table("Employees", Schema = "Test")]
    public sealed class Employee
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "integer")]
        public int? UserId { get; set; }
        public bool IsDeleted { get; set; }
        public User User { get; set; }
        public ICollection<Sheet> Sheets { get; set; }
    }
}
