using System.ComponentModel.DataAnnotations.Schema;

namespace Timesheets.Entities
{
    [Table("Employees", Schema = "Test")]
    public sealed class Employee : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
