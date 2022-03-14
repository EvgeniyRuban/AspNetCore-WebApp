using System;

namespace Timesheets.Entities.Dto
{
    /// <summary>
    /// A DTO class that provides an User model to response for the client.
    /// </summary>
    public sealed class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? Age { get; set; }
    }
}
