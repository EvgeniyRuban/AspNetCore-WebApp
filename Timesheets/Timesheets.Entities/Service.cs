using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timesheets.Entities
{
    [Table("Services", Schema = "Test")]
    public sealed class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Sheet> Sheets { get; set; }
    }
}
