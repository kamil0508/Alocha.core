using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alocha.WebUi.Models.ManagementAdminVM;
using Alocha.WebUi.Services.Interfaces;
using AutoMapper.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Alocha.WebUi.Controllers
{
    public class ManagementAdminController : Controller
    {
        private readonly IManagementAdminService _managementAdminService;

        public ManagementAdminController(IManagementAdminService managementAdminService)
        {
            _managementAdminService = managementAdminService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ManagementIndexVM()
            {
                Users = await _managementAdminService.GetAllUsersAsync()
            };
            return View(model);
        }
    }
}