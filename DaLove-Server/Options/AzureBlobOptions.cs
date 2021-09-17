using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaLove_Server.Options
{
    public class AzureBlobOptions
    {
        public string ConnectionString { get; set; }
        public string MemoryContainer { get; set; }
    }
}
