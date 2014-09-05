using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace ResyRoom.Infraestructura
{
    public static class AutoMapperConfiguration
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>
             (this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = Mapper.GetAllTypeMaps()
                                     .First(x => x.SourceType == sourceType && x.DestinationType == destinationType);
            
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
                expression.ForMember(property, opt => opt.Ignore());
            
            return expression;
        }
     
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExistingSource<TSource, TDestination>
             (this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = Mapper.GetAllTypeMaps()
                                     .First(x => x.SourceType == sourceType && x.DestinationType == destinationType);
            
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
                expression.ForSourceMember(property, opt => opt.Ignore());
            
            return expression;
        }
    }
}