using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using DaLove_Server.Options;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaLove_Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckSecretWorkingController : ControllerBase
    {
        private readonly KeyVaultOptions _keyVaultOptions;

        public CheckSecretWorkingController(KeyVaultOptions keyVaultOptions)
        {
            _keyVaultOptions = keyVaultOptions;
        }


        [HttpGet]
        public async Task<ActionResult> Check()
        {
            try
            {

                SecretClient client = new SecretClient(new Uri(_keyVaultOptions.KeyVaultUri), new DefaultAzureCredential());

                var secret = await client.GetSecretAsync("secret");

                return Ok(secret);
            }
            catch(Exception e)
            {
                // The whole controller will be removed anyway
                return Ok(e.Message);
            }
        }
    }
}
