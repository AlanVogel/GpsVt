using AutoMapper;
using SpeedSight.Dto;
using SpeedSight.Models;

namespace SpeedSight.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<GpsData, GpsDataDto>().ReverseMap();
        }
    }
}
