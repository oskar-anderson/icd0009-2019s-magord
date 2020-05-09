﻿using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class RestaurantServiceMapper : BaseMapper<DALAppDTO.Restaurant, BLLAppDTO.Restaurant>, IRestaurantServiceMapper
    {
        public RestaurantServiceMapper(): base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Area, BLLAppDTO.Area>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
    }
}