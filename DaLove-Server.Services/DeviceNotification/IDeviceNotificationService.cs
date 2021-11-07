using DaLove_Server.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Services.DeviceNotification
{
    public interface IDeviceNotificationService
    {
        Task PushNotificationNewMemory(UserProfile sender, IEnumerable<UserProfile> userProfiles);
    }
}
