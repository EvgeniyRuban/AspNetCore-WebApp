using System;

namespace Timesheets.Entities.Dto
{
    /// <summary>
    /// Provides an Employee model for a client request.
    /// </summary>
    public sealed class EmployeeRequest
    {
        public Guid? UserId { get; set; }
    }
}
