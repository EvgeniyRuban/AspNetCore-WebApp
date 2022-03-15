using System;s
using System.ComponentModel.DataAnnotations.Schema;

namespace Timesheets.Entities
{
    [Table("Invocies", Schema = "Test")]
    public sealed class Invoice
    {
        public Guid Id { get; set; }

        [InverseProperty("Id")]
        [ForeignKey(nameof(Contract))]
        public Guid ContractId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public Money Sum { get; set; }
        public Contract Contract { get; set; }
    }
}
