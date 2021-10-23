using AutoMapper;
using DaLove_Server.Data.Domain;
using DaLove_Server.Data.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DaLove_Server.MappingProfile
{
    public class UserIdResolver : IValueResolver<UserProfileCreateDto, UserProfile, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIdResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public string Resolve(UserProfileCreateDto source, UserProfile destination, string destMember, ResolutionContext context)
        {
            return _httpContextAccessor.HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
