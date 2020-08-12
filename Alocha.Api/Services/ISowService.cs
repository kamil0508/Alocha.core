using Alocha.Api.DTOs.SowDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.Api.Services
{
    public interface ISowService
    {
        Task<IEnumerable<SowDTO>> GetAllSowsAsync(string email);
        Task<SowOneDTO> GetOneSowAsync(string email, int sowId);
    }
}
