using DaLove_Server.Data;
using DaLove_Server.Data.Domain;
using DaLove_Server.Data.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Services.RandomMemoriesAccess
{
    public interface IMemoryContainerService
    {
        /// <summary>
        /// Return an URI that should be publicly available so the callee can access the memory content
        /// </summary>
        /// <param name="memory">A valid memory, should never be null</param>
        /// <returns>a valid URI</returns>
        public Uri GetUriAccessToMemory(UserMemory memory);

        Uri PostNewMemory(Stream stream, string memoryUniqueName);
    }
}
