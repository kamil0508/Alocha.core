using Alocha.Api.DTOs.SowDTOs;
using Alocha.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.Api.Helpers
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Sow, SowDTO>();
            CreateMap<Sow, SowOneDTO>();
        }
    }
}
