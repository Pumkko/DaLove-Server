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

        /// <summary>
        /// Creator of the memory
        /// </summary>
        [Required]
        public string UserId { get; init; }

        public string MemoryFriendlyName { get; set; }

        [Required]
        public string MemoryUniqueName { get; set; }

        /// <summary>
        /// list of the users who can access the memory, the creator is among them
        /// </summary>
        public virtual ICollection<UserProfile> Recipients { get; set; }
    }
}
