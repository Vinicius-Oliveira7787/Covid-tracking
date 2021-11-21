using AutoMapper;
using CovidTracking.API.Client.Models;
using CovidTracking.Data.Models;

namespace CovidTracking.API.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<CountryData, Country>();
        }
    }
}
