﻿using AutoMapper;
using DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class DALMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TLeftObject : class?, new()
        where TRightObject : class?, new()
    {
        public DALMapper() : base()
        {
            // From DALAppDTO to Domain
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Price, Domain.Price>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Restaurant, Domain.Restaurant>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.OrderType, Domain.OrderType>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.PaymentType, Domain.PaymentType>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Identity.AppUser, Domain.Identity.AppUser>();
            
            
            // From Domain to DALAppDTO
            MapperConfigurationExpression.CreateMap<Domain.OrderItem, DAL.App.DTO.OrderItem>();
            MapperConfigurationExpression.CreateMap<Domain.Price, DAL.App.DTO.Price>();
            MapperConfigurationExpression.CreateMap<Domain.PaymentType, DAL.App.DTO.PaymentType>();
            MapperConfigurationExpression.CreateMap<Domain.OrderType, DAL.App.DTO.OrderType>();
            MapperConfigurationExpression.CreateMap<Domain.Restaurant, DAL.App.DTO.Restaurant>();
            MapperConfigurationExpression.CreateMap<Domain.Campaign, DAL.App.DTO.Campaign>();
            MapperConfigurationExpression.CreateMap<Domain.Ingredient, DAL.App.DTO.Ingredient>();
            MapperConfigurationExpression.CreateMap<Domain.Order, DAL.App.DTO.Order>();
            MapperConfigurationExpression.CreateMap<Domain.Drink, DAL.App.DTO.Drink>();
            MapperConfigurationExpression.CreateMap<Domain.Food, DAL.App.DTO.Food>();
            MapperConfigurationExpression.CreateMap<Domain.FoodType, DAL.App.DTO.FoodType>();
            MapperConfigurationExpression.CreateMap<Domain.Area, DAL.App.DTO.Area>();
            MapperConfigurationExpression.CreateMap<Domain.Town, DAL.App.DTO.Town>();
            MapperConfigurationExpression.CreateMap<Domain.Identity.AppUser, DAL.App.DTO.Identity.AppUser>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
    }
}