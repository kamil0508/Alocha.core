using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.Api.DTOs.AuthenticationDTOs
{
    public class AccessTokenDTO
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
