using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Data.Dtos
{
    public record PostMemoryDto
    {
        public IEnumerable<string> Recipients { get; init; }
        
        public string MemoryCaption { get; init; }

    }
}
