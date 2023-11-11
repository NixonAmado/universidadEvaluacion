using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserWithRolDto>()
        .ForMember(dest => dest.RolName, opt => opt.MapFrom(src => src.Roles.FirstOrDefault().Name))
        .ReverseMap();

        CreateMap<Asignatura, BsAsignaturaDto>().ReverseMap();   
        CreateMap<Asignatura, NombreAsignaturaDto>().ReverseMap();
        
        CreateMap<CursoEscolar,BsCursoEscolarDto>().ReverseMap();   
        CreateMap<CursoEscolar,MatriculaCursoEscolarDto>().ReverseMap();   
        
        CreateMap<Departamento,BsDepartamentoDto>().ReverseMap();   
        CreateMap<Departamento,ProfesorDepartamentoDto>().ReverseMap();   

        CreateMap<Matricula,MatriculaCursoEscolarDto>().ReverseMap();   
        CreateMap<Matricula,BsAsignaturaDto>().ReverseMap();   
        
        CreateMap<Persona,BsPersonaDto>().ReverseMap();   
        CreateMap<Persona,PointUno>().ReverseMap();
        CreateMap<Profesor,DepartamentoProfesorDto>().ReverseMap();
        CreateMap<Profesor,BsProfesorDto>().ReverseMap();   

        CreateMap<TipoAsignatura,BsTipoAsignaturaDto>().ReverseMap();   
        CreateMap<Grado,BsGradoDto>().ReverseMap();  

        CreateMap<AlumnoAsignatura,AlumnoAsignaturaDto>().ReverseMap();   

    

    }
}