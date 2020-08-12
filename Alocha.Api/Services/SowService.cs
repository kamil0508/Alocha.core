using Alocha.Api.DTOs.SowDTOs;
using Alocha.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.Api.Services
{
    public class SowService : ISowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SowService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SowDTO>> GetAllSowsAsync(string email)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Email == email);
            if (user != null)
            {
                var sows = user.Sows.Where(s => !s.IsRemoved);
                var dto = _mapper.Map<IEnumerable<SowDTO>>(sows);
                return dto;
            }
            return null;
        }

        public async Task<SowOneDTO> GetOneSowAsync(string email, int sowId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Email == email);
            if (user != null)
            {
                var sow = user.Sows.Where(s => s.SowId == sowId && !s.IsRemoved).First();
                var dto = _mapper.Map<SowOneDTO>(sow);
                return dto;
            }
            return null;
        }
    }
}
