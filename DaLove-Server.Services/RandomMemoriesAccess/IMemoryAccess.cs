using DaLove_Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Services.RandomMemoriesAccess
{
    public interface IMemoryAccess
    {
        /// <summary>
        /// Return an URI that should be publicly available so the callee can access the memory content
        /// </summary>
        /// <param name="memory">A valid memory, should never be null</param>
        /// <returns>a valid URI</returns>
        public Uri GetUriAccessToMemory(UserMemory memory);
    }
}
