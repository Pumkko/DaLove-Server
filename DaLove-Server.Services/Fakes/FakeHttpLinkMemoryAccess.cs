using DaLove_Server.Data;
using DaLove_Server.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Services.RandomMemoriesAccess
{
    public class FakeHttpLinkMemoryAccess : IMemoryAccessService
    {
        public Uri GetUriAccessToMemory(UserMemory memory)
        {
            return new Uri("https://file-examples-com.github.io/uploads/2017/04/file_example_MP4_640_3MG.mp4");
        }
    }
}
