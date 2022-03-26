using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timesheets.Entities
{
    [Table("Invocies", Schema = "Test")]
    public sealed class Invoice
    {
        [Key]
        public int Id { get; set; }
        public int ContractId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public Money Sum { get; set; }
        public Contract Contract { get; set; }
    }
}
