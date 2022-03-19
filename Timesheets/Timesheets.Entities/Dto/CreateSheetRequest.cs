using System;
using System.ComponentModel.DataAnnotations;

namespace Timesheets.Entities.Dto
{
    public sealed class CreateSheetRequest
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int ContractId { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [Required]
        public int InvoiceId { get; set; }
        public int Amount { get; set; }
    }
}
