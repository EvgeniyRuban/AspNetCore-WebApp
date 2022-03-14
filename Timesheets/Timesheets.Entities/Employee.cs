using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timesheets.Entities
{
    [Table("Employees", Schema = "Test")]
    public sealed class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [InverseProperty("Id")]
        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }
        public bool IsDeleted { get; set; }
    }
}
