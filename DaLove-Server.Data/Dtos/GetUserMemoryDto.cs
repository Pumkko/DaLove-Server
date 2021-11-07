using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Data.Dtos
{
    public record GetUserMemoryDto
    {
        public Uri MemoryUri { get; init; }

        public string MemoryFriendlyName { get; init; }

        public UserProfileGetDto Creator { get; init; }
    }
}
