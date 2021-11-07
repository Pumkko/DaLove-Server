using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Data.Dtos
{
    public record PostFcmDto
    {
        public string NewTcmDeviceToken { get; init; }
    }
}
