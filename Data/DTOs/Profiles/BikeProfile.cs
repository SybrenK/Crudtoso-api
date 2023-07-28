using AutoMapper;
using Crudtoso_api.Data.DTOs.BikeDTOs;
using Crudtoso_api.Model;

namespace Crudtoso_api.Data.DTOs.Profiles
{
    public class BikeProfile : Profile
    {
        public BikeProfile()
        {
            CreateMap<BikeDb, BikeReadDTO>();
            CreateMap<BikeCreateDTO, BikeDb>();
            CreateMap<BikeUpdateDTO, BikeDb>();
        }
    }
}
