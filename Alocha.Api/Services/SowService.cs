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
                var model = _mapper.Map<IEnumerable<SowDTO>>(sows);
                return model;
            }
            throw new NullReferenceException();
        }
    }
}
