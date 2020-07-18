using Alocha.Domain.Entities;
using Alocha.WebUi.Models.AccountVM;
using Alocha.WebUi.Models.SowVM;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Helpers
{
    public class AutoMaperConfiguration : Profile
    {
        public AutoMaperConfiguration()
        {
            CreateMap<Sow, SowVM>();
            CreateMap<SowIndexVM, Sow>();
            CreateMap<Sow, SowEditVM>().ReverseMap();
            CreateMap<SowEditVM, SmallPig>();
        }
    }
}
