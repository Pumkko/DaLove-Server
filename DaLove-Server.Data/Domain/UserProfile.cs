using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Data.Domain
{
    public record UserProfile
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UniqueUserName { get; set; }

        [Required]
        public string UserId { get; init; }

        [Required]
        public string DisplayName { get; set; }

        public string AvatarFileName { get; set; }

        public virtual ICollection<UserMemory> Memories { get; set; }
    }

}
