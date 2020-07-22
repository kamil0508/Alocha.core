using Alocha.Domain.Entities;
using Alocha.WebUi.Models.AccountVM;
using Alocha.WebUi.Models.HomeVM;
using Alocha.WebUi.Models.SmallPigsVM;
using Alocha.WebUi.Models.SowVM;
using Alocha.WebUi.Models.UserVM;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
            CreateMap<SmallPig, SmallPigVM>();
            CreateMap<Sow, SowDetailVM>()
                .ForMember(dest => dest.SmallPigs, opt => opt.MapFrom(src => src.SmallPigs.Where(s => !s.IsRemoved)));
            CreateMap<Sow, UpcomingTaskVM>();

            CreateMap<IdentityUser, UserManageVM>();

        }
    }
}
