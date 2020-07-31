using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alocha.WebUi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alocha.WebUi.Controllers
{
    [Authorize]
    public class SmallPigController : Controller
    {
        private readonly ISmallPigService _smallPigService;

        public SmallPigController(ISmallPigService smallPigService)
        {
            _smallPigService = smallPigService;
        }

        public async Task<string> Remove(int id)
        {
            var result = await _smallPigService.RemoveSmallPigAsync(id);
            if (result)
                return null;
            return "Nie udało się usunąć wpisu.";
        }
    }
}