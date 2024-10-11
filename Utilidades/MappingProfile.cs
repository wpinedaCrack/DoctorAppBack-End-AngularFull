using AutoMapper;
using Models.DTOs;
using Models.Entidades;

namespace Utilidades
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Especialidad, EspecialidadDto>()
                .ForMember(d => d.Estado, m => m.MapFrom(o => o.Estado == true ? 1 : 0));

            CreateMap<Medico, MedicoDto>()
               .ForMember(d => d.Estado, m => m.MapFrom(o => o.Estado == true ? 1 : 0))
               .ForMember(d => d.NombreEspecialidad, m => m.MapFrom(o => o.Especialidad.NombreEspecialidad));
        }
    }
}
