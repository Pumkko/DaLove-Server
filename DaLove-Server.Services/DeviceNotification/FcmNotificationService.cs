﻿using DaLove_Server.Data.Domain;
using DaLove_Server.Options;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Services.DeviceNotification
{
    public class FcmNotificationService : IDeviceNotificationService
    {
        private readonly GoogleFcmOptions _fcmOptions;

        public FcmNotificationService(GoogleFcmOptions fcmOptions)
        {
            _fcmOptions = fcmOptions;
        }

        public async Task PushNotificationNewMemory(UserProfile sender, IEnumerable<UserProfile> userProfiles)
        {
            var app = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromJson(_fcmOptions.JsonCredentials)
            });

            var messaging = FirebaseMessaging.GetMessaging(app);

            var messages = userProfiles
                .Where(u => !string.IsNullOrEmpty(u.LastKnownFcmDeviceToken))
                .Select(u => new Message()
                {
                    Token = u.LastKnownFcmDeviceToken,
                    Notification = new()
                    {
                        Title = "Someone thought about you !",
                        Body = $"{sender.DisplayName} just shared a memory with you",
                    }
                });

            await messaging.SendAllAsync(messages);
        }
    }
}
