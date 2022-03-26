using System;

namespace Timesheets.Entities.Dto
{
    public sealed class ContractResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
