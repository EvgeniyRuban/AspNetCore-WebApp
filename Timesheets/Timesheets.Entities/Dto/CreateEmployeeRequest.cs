using System.ComponentModel.DataAnnotations;

namespace Timesheets.Entities.Dto
{
    /// <summary>
    /// Provides an Employee model for a client requesting the creation of this model.
    /// </summary>
    public sealed class CreateEmployeeRequest
    {
        [MinLength(1)]
        public int UserId { get; set; }
    }
}
