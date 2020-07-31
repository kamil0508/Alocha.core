﻿using Alocha.Domain.Entities;
using Alocha.Domain.Interfaces;
using Alocha.WebUi.Models.ManagementAdminVM;
using Alocha.WebUi.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Services
{
    public class ManagementAdminService : IManagementAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ManagementAdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ManagementVM>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.User.GetAllAsync();
            var model = _mapper.Map<IEnumerable<User>, IEnumerable<ManagementVM>>(users);
            return model;
        }
    }
}
