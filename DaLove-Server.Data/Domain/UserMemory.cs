using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaLove_Server.Data.Domain
{
    public class UserMemory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; init; }

        [Required]
        public string UserId { get; init; }

        [Required]
        public string MemoryName { get; set; }
    }
}
