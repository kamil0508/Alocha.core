using Alocha.Domain.Interfaces;
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

        public async Task<bool> RemoveSmallPigAsync(int smallPigId)
        {
            var smallPig = await _unitOfWork.Smallpig.GetByIdAsync(smallPigId);
            smallPig.IsRemoved = true;
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
