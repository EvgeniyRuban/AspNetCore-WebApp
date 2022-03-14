using System;

namespace Timesheets.Entities.Dto
{
    /// <summary>
    /// Provides an Employee model to response for the client.
    /// </summary>
    public sealed class EmployeeResponse
    {
        public Guid Id { get; set; }
        public UserResponse User { get; set; }
    }
}
