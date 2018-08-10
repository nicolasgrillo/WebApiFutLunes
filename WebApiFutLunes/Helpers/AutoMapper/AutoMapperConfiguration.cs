using AutoMapper;
using WebApiFutLunes.Data.DTOs;
using WebApiFutLunes.Models.Player;

namespace WebApiFutLunes.Helpers.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<ApplicationUser, PlayerModel>();
            });
        }
    }
}