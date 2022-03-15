using System;

namespace Timesheets.Entities.Dto
{
    public sealed class InvoiceRequest
    {
        public int ContractId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public Money Sum { get; set; }
    }
}
