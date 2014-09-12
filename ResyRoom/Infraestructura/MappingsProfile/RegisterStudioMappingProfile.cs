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

                CreateMap<RoomScheduleViewModel, Horario>()
                    .ForMember(dest => dest.DuracionBloque, opt => opt.MapFrom(src => src.DuracionBloque))
                    .ForMember(dest => dest.DíasAbierto, opt => opt.MapFrom(src => src.DiasAbierto))
                    .ForMember(dest => dest.FechaIngreso, opt => opt.MapFrom(src => DateTime.Now))
                    .ForMember(dest => dest.HoraCierre, opt => opt.MapFrom(src => src.HoraCierre))
                    .ForMember(dest => dest.HoraApertura, opt => opt.MapFrom(src => src.HoraApertura))
                    .IgnoreAllNonExisting()
                    .ReverseMap()
                    .ForMember(dest => dest.DuracionBloque, opt => opt.MapFrom(src => src.DuracionBloque))
                    .ForMember(dest => dest.DiasAbierto, opt => opt.MapFrom(src => src.DíasAbierto))
                    .ForMember(dest => dest.HoraCierre, opt => opt.MapFrom(src => src.HoraCierre))
                    .ForMember(dest => dest.HoraApertura, opt => opt.MapFrom(src => src.HoraApertura))
                    .IgnoreAllNonExistingSource();

                CreateMap<RoomScheduleViewModel, Sala>()
                    .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.PrecioPorBloque))
                    .IgnoreAllNonExisting()
                    .ReverseMap()
                    .ForMember(dest => dest.PrecioPorBloque, opt => opt.MapFrom(src => src.Precio))
                    .IgnoreAllNonExistingSource();

                CreateMap<RoomEquipmentViewModel, Equipo>()
                    .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                    .ForMember(dest => dest.Fotografia, opt => opt.MapFrom(src => src.Fotografia))
                    .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                    .ForMember(dest => dest.PrecioAdicional, opt => opt.MapFrom(src => src.PrecioAdicional))
                    .IgnoreAllNonExisting()
                    .ReverseMap()
                    .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                    .ForMember(dest => dest.Fotografia, opt => opt.MapFrom(src => src.Fotografia))
                    .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                    .ForMember(dest => dest.PrecioAdicional, opt => opt.MapFrom(src => src.PrecioAdicional))
                    .IgnoreAllNonExistingSource();
            }
        }
    }
}