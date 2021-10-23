using System;

namespace DaLove_Server.Data.Dtos
{
    public record UserProfileGetDto
    {
        public string DisplayName { get; init; }

        public string UniqueUserName { get; init; }

        public Uri AvatarUri { get; init; }
    }
}
