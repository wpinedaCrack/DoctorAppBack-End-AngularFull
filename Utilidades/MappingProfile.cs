using AutoMapper;
using Models.DTOs;
using Models.Entidades;

namespace Utilidades
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            CreateMap<Especialidad, EspecialidadDto>()
                .ForMember(d=>d.Estado, m => m.MapFrom(o=>o.Estado == true ? 1:0));
        }

    }
}
