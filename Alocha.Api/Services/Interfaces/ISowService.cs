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
        Task<SowOneDTO> AddSowAsync(SowCreateDTO dto, string email);
        Task<bool> EditSowAsync(SowOneDTO dto, string email);
        Task<bool> RemoveSowAync(string email, int sowId);
        Task<IEnumerable<SowDTO>> GetPregnantSows(string email);
        Task<bool> VaccinateSow(int sowId, string email);
    }
}
