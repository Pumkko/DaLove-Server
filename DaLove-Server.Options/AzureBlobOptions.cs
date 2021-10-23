using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaLove_Server.Options
{
    public class AzureBlobOptions
    {
        public string ConnectionString { get; init; }
        public string MemoryContainer { get; init; }
        public string AvatarContainer { get; init; }
    }
}
