using Alocha.Domain.Entities;
using Alocha.Domain.Interfaces;
using Alocha.WebUi.Models.SmallPigsVM;
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

        public async Task<bool> CreateSowAsync(SowCreateVM model, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            if (!user.Sows.Where(s => s.Number == model.Number && !s.IsRemoved).Any())
            {
                var sow = _mapper.Map<SowCreateVM, Sow>(model);
                sow.UserId = userId;
                sow.DateHappening = DateTime.Now;
                _unitOfWork.Sow.Add(sow);
                return await _unitOfWork.SaveChangesAsync();
            }
            return false;
        }

        public async Task<SowEditVM> GetSowForEditAsync(int sowId, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            var sow = user.Sows.Where(s => s.SowId == sowId && !s.IsRemoved).FirstOrDefault();
            var model = _mapper.Map<Sow, SowEditVM>(sow);
            return model;
        }

        public async Task<bool> EditSowAsync(SowEditVM model)
        {
            var sow = await _unitOfWork.Sow.GetByIdAsync(model.SowId);
            switch (model.Status)
            {
                case "Prośna":
                    model.DateBorn = model.DateHappening.AddDays(114);
                    model.VaccineDate = model.DateBorn.Value.AddDays(-model.VaccineDays);
                    break;
                case "Luźna":
                    model.DateInsimination = model.DateHappening.AddDays(8);
                    model.IsVaccinated = false;
                    break;
                case "Laktacja":
                    model.DateDetachment = model.DateHappening.AddDays(28);
                    model.IsVaccinated = false;
                    //add smallpig
                    if (model.PigsQuantity != null)
                    {
                        model.BornDate = model.DateHappening;
                        var smallpig = _mapper.Map<SowEditVM, SmallPig>(model);
                        _unitOfWork.Smallpig.Add(smallpig);
                    }
                    break;

            }

            _mapper.Map(model, sow);
            return await _unitOfWork.SaveChangesAsync();
        }

        public string CalculateDate(DateTime date, string status)
        {
            if (status == "Prośna")
                return date.AddDays(114).ToShortDateString();
            else if (status == "Luźna")           
                return date.AddDays(8).ToShortDateString();
            else 
                return date.AddDays(28).ToShortDateString();                          
        }

        public async Task<bool> RemoveSowAsync(int sowId)
        {
            var sow = await _unitOfWork.Sow.GetByIdAsync(sowId);
            sow.IsRemoved = true;
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<SowDetailVM> DetailsSowAsync(int sowId, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            var sow = user.Sows.Where(s => s.SowId == sowId && !s.IsRemoved).FirstOrDefault();
            var model = _mapper.Map<Sow, SowDetailVM>(sow);
            return model;
        }

        public async Task<IEnumerable<SowVM>> GetPregnancySows(string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            var sows = user.Sows.Where(s => s.Status == "Prośna" && !s.IsRemoved);
            var model = _mapper.Map<IEnumerable<Sow>, IEnumerable<SowVM>>(sows);
            return model;
        }

        public async Task<bool> SowWasVacinated(int sowId, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            var sow = user.Sows.Where(s => s.SowId == sowId && !s.IsRemoved).FirstOrDefault();
            sow.IsVaccinated = true;
            return await _unitOfWork.SaveChangesAsync();
        }

        public void CalcualteCountAndAverageSmallPigs(IEnumerable<SmallPigVM> model, ref double average, ref int count)
        {
            average = Math.Round(model.Average(s => s.PigsQuantity));
            count = model.Count();
        }
    }
}
