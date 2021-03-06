using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Services.Avatar
{
    public interface IAvatarAccessService
    {
        Uri StoreAvatar(string avatarGuid, Stream fileToUpload);

        Uri GetAvatar(string avatarFileName);

        void RemoveAvatarFile(string avatarFileName);
    }
}
