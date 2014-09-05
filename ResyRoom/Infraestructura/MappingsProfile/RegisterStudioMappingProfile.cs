using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ResyRoom.Models;
using ResyRoom.ViewModels.RegisterStudio;

namespace ResyRoom.Infraestructura.MappingsProfile
{
    public class RegisterStudioMappingProfile : Profile
    {
        private const string ViewModel = "RegisterStudioViewModel";

        public override string ProfileName
        {
            get { return ViewModel; }
        }

        protected override void Configure()
        {
            {
                CreateMap<StudioViewModel, Estudio>()
                    .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                    .ForMember(dest => dest.Celular, opt => opt.MapFrom(src => src.Celular))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
                    .ForMember(dest => dest.UrlName, opt => opt.MapFrom(src => src.UrlName))
                    .IgnoreAllNonExisting()
                    .ReverseMap()
                    .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                    .ForMember(dest => dest.Celular, opt => opt.MapFrom(src => src.Celular))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
                    .ForMember(dest => dest.UrlName, opt => opt.MapFrom(src => src.UrlName))
                    .IgnoreAllNonExistingSource();

                CreateMap<RoomViewModel, Sala>()
                    .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                    .ForMember(dest => dest.Tamaño, opt => opt.MapFrom(src => src.Tamaño))
                    .IgnoreAllNonExisting()
                    .ReverseMap()
                    .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                    .ForMember(dest => dest.Tamaño, opt => opt.MapFrom(src => src.Tamaño))
                    .IgnoreAllNonExistingSource();
            }
        }
    }
}