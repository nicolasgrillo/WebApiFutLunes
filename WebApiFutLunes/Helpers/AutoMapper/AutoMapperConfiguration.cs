using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using WebApiFutLunes.Data.Models;
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