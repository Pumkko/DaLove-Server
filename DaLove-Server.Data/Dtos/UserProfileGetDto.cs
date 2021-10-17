using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Data.Dtos
{
    public record UserProfileGetDto
    {
        public string DisplayName { get; set; }

        public string UniqueUserName { get; set; }
    }
}
