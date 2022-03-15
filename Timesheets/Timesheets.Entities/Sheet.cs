using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timesheets.Entities
{
    [Table("Sheets", Schema = "Test")]
    public sealed class Sheet
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        [InverseProperty("Id")]
        [ForeignKey(nameof(Employee))]
        public Guid EmployeeId { get; set; }

        [InverseProperty("Id")]
        [ForeignKey(nameof(Contract))]
        public Guid ContractId { get; set; }

        [InverseProperty("Id")]
        [ForeignKey(nameof(Service))]
        public Guid ServiceId { get; set; }

        [InverseProperty("Id")]
        [ForeignKey(nameof(Invoice))]
        public Guid InvoiceId { get; set; }
        public int Amount { get; set; }
        public bool IsApproved { get; set; }
        public DateTime ApprovedDate { get; set; }

        public Employee Employee { get; set; }
        public Contract Contract { get; set; }
        public Service Service { get; set; }
        public Invoice Invoice { get; set; }    
    }
}
