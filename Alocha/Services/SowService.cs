using Alocha.Domain.Entities;
using Alocha.Domain.Interfaces;
using Alocha.WebUi.Models.SowVM;
using Alocha.WebUi.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Services
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

        public async Task<IEnumerable<SowVM>> GetAllSowsAsync(string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            if(user != null)
            {
                if(user.Sows.Count() > 0)
                {
                    var sows = user.Sows.Where(s => !s.IsRemoved);
                    var model = _mapper.Map<IEnumerable<Sow>, IEnumerable<SowVM>>(sows);
                    return model;
                }
            }
            return new List<SowVM>();
        }

        public async Task<bool> CreateSowAsync(SowIndexVM model, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            if (!user.Sows.Where(s => s.Number == model.Number).Any())
            {
                var sow = _mapper.Map<SowIndexVM, Sow>(model);
                sow.UserId = userId;
                _unitOfWork.Sow.Add(sow);
                return await _unitOfWork.SaveChangesAsync();
            }
            return false;
        }
    }
}
