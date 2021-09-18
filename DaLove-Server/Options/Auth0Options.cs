using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaLove_Server.Options
{
    public record Auth0Options
    {
        public string Authority { get; init; }

        public string Audience { get; init; }
    }
}
