using AutoMapper;
using DaLove_Server.Data.Domain;
using DaLove_Server.Data.Dtos;
using DaLove_Server.Services.Avatar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaLove_Server.MappingProfile
{
    public class AvatarUriResolver : IValueResolver<UserProfile, UserProfileGetDto, Uri>
    {

        private readonly IAvatarAccessService _avatarAccessService;

        public AvatarUriResolver(IAvatarAccessService avatarAccessService)
        {
            _avatarAccessService = avatarAccessService;
        }


        public Uri Resolve(UserProfile source, UserProfileGetDto destination, Uri destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.AvatarFileName))
            {
                return  _avatarAccessService.GetAvatar(source.AvatarFileName);
            }

            return null;
        }
    }
}
