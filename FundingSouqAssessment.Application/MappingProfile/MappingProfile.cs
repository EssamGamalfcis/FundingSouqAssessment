using AutoMapper;
using FundingSouqAssessment.Domain.Entities;
using FundingSouqAssessment.Models;

namespace FundingSouqAssessment.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateClientDto, Client>();
            CreateMap<UpdateClientDto, Client>();
        }
    }
}
