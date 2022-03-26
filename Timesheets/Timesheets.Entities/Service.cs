using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timesheets.Entities
{
    [Table("Services", Schema = "Test")]
    public sealed class Service
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Sheet> Sheets { get; set; }
    }
}
