using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaLove_Server.Data.Domain
{
    public record UserMemory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; init; }

        [Required]
        public string UserId { get; init; }

        public string MemoryFriendlyName { get; set; }

        [Required]
        public string MemoryUniqueName { get; set; }

        public virtual ICollection<UserProfile> Recipients { get; set; }
    }
}
