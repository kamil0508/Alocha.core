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
    public class SmallPigService : ISmallPigService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SmallPigService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddSmallPig(SowEditVM sowEditVM)
        {
            sowEditVM.BornDate = sowEditVM.DateHappening;
            var smallpig = _mapper.Map<SmallPig>(sowEditVM);
            _unitOfWork.Smallpig.Add(smallpig);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> RemoveSmallPigAsync(int smallPigId)
        {
            var smallPig = await _unitOfWork.Smallpig.GetByIdAsync(smallPigId);
            smallPig.IsRemoved = true;
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
