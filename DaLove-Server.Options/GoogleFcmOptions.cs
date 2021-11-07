using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Options
{
    public record GoogleFcmOptions
    {
        public string JsonCredentials { get; init; }
    }
}
