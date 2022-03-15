using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timesheets.Entities
{
    [Table("Clients", Schema = "Test")]
    public sealed class Client
    {
        public Guid Id { get; set; }

        [InverseProperty("Id")]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
        public User User { get; set; }
    }
}
