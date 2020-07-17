using Alocha.Domain.Entities;
using Alocha.Domain.Interfaces;
using Alocha.WebUi.Models.SowVM;
using Alocha.WebUi.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
                sow.DateHappening = DateTime.Now;
                _unitOfWork.Sow.Add(sow);
                return await _unitOfWork.SaveChangesAsync();
            }
            return false;
        }

        public async Task<SowEditVM> GetSowForEditAsync(int sowId)
        {
            var sow = await _unitOfWork.Sow.GetByIdAsync(sowId);
            var model = _mapper.Map<Sow, SowEditVM>(sow);
            return model;
        }

        public async Task<bool> EditSowAsync(SowEditVM model)
        {
            var sow = await _unitOfWork.Sow.GetByIdAsync(model.SowId);
            if(model.Status == "Prośna")
            {
                model.DateBorn = model.DateHappening.AddDays(114);
                model.VaccineDate = model.DateBorn.Value.AddDays(-model.VaccineDays);
            }
            if (model.Status == "Luźna")
                model.DateInsimination = model.DateHappening.AddDays(8);
            if (model.Status == "Laktacja")       
                model.DateDetachment = model.DateHappening.AddDays(28);
          
            _mapper.Map(model, sow);
            return await _unitOfWork.SaveChangesAsync();
        }

        public string CalculateDate(DateTime date, string status)
        {
            var response = "";
            if (status == "Prośna")
                response = date.AddDays(114).ToShortDateString();
            if (status == "Luźna")           
                response = date.AddDays(8).ToShortDateString();
            if(status == "Laktacja")
                response = date.AddDays(28).ToShortDateString();                          
            return response;
        }
    }
}
