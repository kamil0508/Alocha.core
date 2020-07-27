using Alocha.Domain.Entities;
using Alocha.Domain.Interfaces;
using Alocha.WebUi.Models.HomeVM;
using Alocha.WebUi.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Services
{
    public class HomeService : IHomeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UpcomingTaskVM>> GetUpcomingTaskAsync(string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            var sevenDaysUp = DateTime.Today.AddDays(7);
            var sevenDaysAgo = DateTime.Today.AddDays(-7);

            var sows = user.Sows.Where(s => !s.IsRemoved && s.DateBorn >= sevenDaysAgo && s.DateBorn <= sevenDaysUp
                        || !s.IsRemoved && s.DateDetachment >= sevenDaysAgo && s.DateDetachment <= sevenDaysUp
                        || !s.IsRemoved && s.DateInsimination >= sevenDaysAgo && s.DateInsimination <= sevenDaysUp
                        || !s.IsRemoved && s.VaccineDate >= sevenDaysAgo && s.VaccineDate <= sevenDaysUp);

            var model = _mapper.Map<IEnumerable<Sow>, IEnumerable<UpcomingTaskVM>>(sows);
            return model;
        }

        public async Task<string> GetUpcomingTaskCountAsync(string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            var sevenDaysUp = DateTime.Today.AddDays(7);
            var sevenDaysAgo = DateTime.Today.AddDays(-7);

            var count = user.Sows.Where(s => !s.IsRemoved && s.DateBorn >= sevenDaysAgo && s.DateBorn <= sevenDaysUp
                        || !s.IsRemoved && s.DateDetachment >= sevenDaysAgo && s.DateDetachment <= sevenDaysUp
                        || !s.IsRemoved && s.DateInsimination >= sevenDaysAgo && s.DateInsimination <= sevenDaysUp
                        || !s.IsRemoved && s.VaccineDate >= sevenDaysAgo && s.VaccineDate <= sevenDaysUp).Count();

            return count.ToString();
        }
    }
}
