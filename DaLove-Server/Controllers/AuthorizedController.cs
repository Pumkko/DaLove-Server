using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DaLove_Server.Controllers
{
    [Authorize("read:memories")]
    [ApiController]
    public class AuthorizedController : ControllerBase
    {
        public string CurrentUserId
        { 
            get
            {
                return User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            }
        }
    }
}
