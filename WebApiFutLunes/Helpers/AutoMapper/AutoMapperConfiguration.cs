using AutoMapper;
using WebApiFutLunes.Data.DTOs;
using WebApiFutLunes.Models.Match;
using WebApiFutLunes.Models.Player;

namespace WebApiFutLunes.Helpers.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<ApplicationUser, PlayerModel>();

                cfg.CreateMap<AddUpdateMatchModel, AddUpdateMatchDto>()
                    .ForMember(dto => dto.LocationMapUrl, map => map.MapFrom(model => model.LocationMapUrl))
                    .ForMember(dto => dto.LocationTitle, map => map.MapFrom(model => model.LocationTitle))
                    .ForMember(dto => dto.MatchDate, map => map.MapFrom(model => model.MatchDate))
                    .ForMember(dto => dto.PlayerLimit, map => map.MapFrom(model => model.PlayerLimit));
            });
        }
    }
}