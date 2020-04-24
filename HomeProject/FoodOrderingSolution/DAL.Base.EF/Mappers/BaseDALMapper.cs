using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.Base.EF.Mappers
{
    public class BaseDALMapper<TInObject, TOutObject> : IBaseDALMapper<TInObject, TOutObject>
        where TInObject : class, new()
        where TOutObject : class, new()

    {
        private readonly IMapper _mapper;

        public BaseDALMapper()
        {
            _mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<TInObject, TOutObject>();
                config.CreateMap<TOutObject, TInObject>();
            }).CreateMapper();
        }

        public TOutObject Map(TInObject inObject)
        {
            return _mapper.Map<TInObject, TOutObject>(inObject);
        }

        public TMapOutObject Map<TMapInObject, TMapOutObject>(TMapInObject inObject) where TMapInObject : class
            where TMapOutObject : class, new()
        {
            //return _mapper.Map<TMapOutObject>(inObject);

            var inProperties = inObject
                .GetType()
                .GetProperties()
                .ToDictionary(
                    key => key.Name,
                    val => val.GetValue(inObject));

            var result = new TMapOutObject();
            foreach (var property in result.GetType().GetProperties())
            {
                if (inProperties.TryGetValue(property.Name, out var value))
                {
                    // is it a struct or a string
                    if (!property.PropertyType.IsClass && !property.PropertyType.IsInterface)
                    {
                        property.SetValue(result, value);
                        continue;
                    }

                    if (typeof(string).IsAssignableFrom(property.PropertyType))
                    {
                        property.SetValue(result, value);
                        continue;
                    }

                    if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                    {
                        property
                            .SetValue(result,
                                Convert.ChangeType(
                                    ((IEnumerable) value).Cast<object>()
                                    .Select(x => Convert.ChangeType((GetType()
                                        .GetMethod(nameof(Map))
                                        .MakeGenericMethod(property.PropertyType)
                                        .Invoke(this, new[] {x})), property.PropertyType.GetGenericArguments()[0]))
                                    .ToList(), property.PropertyType)
                            );
                        continue;
                    }

                    property.SetValue(result, GetType()
                        .GetMethod(nameof(Map))
                        .MakeGenericMethod(property.PropertyType)
                        .Invoke(this, new[] {value}));
                }
            }
            return result;
        }
    }
}