using AutoMapper;
using PlatformService.DTO;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformsProfile: Profile
    {
        public PlatformsProfile()
        {
            //<x1,x2> -_mapper.Map<x2>(������ x1)
            //x1(��� ��������)
            //x2(�� ��� �� ������������ ������ x1)
            CreateMap<Platform, PlatformReadDTO>();
            CreateMap<PlatformCreateDTO, Platform>();
        }
    }
}