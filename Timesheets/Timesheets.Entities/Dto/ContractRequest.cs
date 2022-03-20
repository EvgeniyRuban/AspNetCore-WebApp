using System;

namespace Timesheets.Entities.Dto
{
    public sealed class ContractRequest
    {
        public string Title { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
