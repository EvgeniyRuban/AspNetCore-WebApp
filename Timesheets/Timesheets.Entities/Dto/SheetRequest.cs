using System;

namespace Timesheets.Entities.Dto
{
    public sealed class SheetRequest
    {
        public DateTime Date { get; set; }
        public int? EmployeeId { get; set; }
        public int? ContractId { get; set; }
        public int? ServiceId { get; set; }
        public int? InvoiceId { get; set; }
        public int Amount { get; set; }
    }
}
