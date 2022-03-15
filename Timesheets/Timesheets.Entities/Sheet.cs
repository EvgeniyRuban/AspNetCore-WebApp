using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timesheets.Entities
{
    [Table("Sheets", Schema = "Test")]
    public sealed class Sheet
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int? EmployeeId { get; set; }
        public int? ContractId { get; set; }
        public int? ServiceId { get; set; }
        public int? InvoiceId { get; set; }
        public int Amount { get; set; }
        public bool IsApproved { get; set; }
        public DateTime ApprovedDate { get; set; }

        public Employee Employee { get; set; }
        public Contract Contract { get; set; }
        public Service Service { get; set; }
        public Invoice Invoice { get; set; }    
    }
}
