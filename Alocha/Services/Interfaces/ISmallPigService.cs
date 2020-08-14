using Alocha.Domain.Entities;
using Alocha.WebUi.Models.SowVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Services.Interfaces
{
    public interface ISmallPigService
    {
        Task<bool> AddSmallPig(SowEditVM sowEditVM);
        Task<bool> RemoveSmallPigAsync(int smallPigId);
    }
}
