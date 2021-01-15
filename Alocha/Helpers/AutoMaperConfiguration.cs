using Alocha.Domain.Entities;
using Alocha.WebUi.Models.AccountVM;
using Alocha.WebUi.Models.HomeVM;
using Alocha.WebUi.Models.ManagementAdminVM;
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
            CreateMap<Sow, SowVM>()
                .ForMember(dest => dest.DateBorn, opt => opt.MapFrom(src => src.DateBorn != null ? src.DateBorn.Value.ToShortDateString() : ""))
                .ForMember(dest => dest.DateHappening, opt => opt.MapFrom(src => src.DateHappening != null ? src.DateHappening.Value.ToShortDateString() : ""))
                .ForMember(dest => dest.DateDetachment, opt => opt.MapFrom(src => src.DateDetachment != null ? src.DateDetachment.Value.ToShortDateString() : ""))
                .ForMember(dest => dest.DateInsimination, opt => opt.MapFrom(src => src.DateInsimination != null ? src.DateInsimination.Value.ToShortDateString() : ""))
                .ForMember(dest => dest.VaccineDate, opt => opt.MapFrom(src => src.VaccineDate != null ? src.VaccineDate.Value.ToShortDateString() : ""));

            CreateMap<SowCreateVM, Sow>();
            CreateMap<Sow, SowEditVM>().ReverseMap();
            CreateMap<SowEditVM, SmallPig>();
            CreateMap<SmallPig, SmallPigVM>();
            CreateMap<Sow, SowDetailVM>()
                .ForMember(dest => dest.SmallPigs, opt => opt.MapFrom(src => src.SmallPigs.Where(s => !s.IsRemoved)));
            CreateMap<Sow, UpcomingTaskVM>();

            CreateMap<IdentityUser, UserManageVM>();

            CreateMap<User, ManagementVM>();

        }
    }
}
