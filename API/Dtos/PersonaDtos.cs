using Domain.Entities;
using static Domain.Entities.Persona;
using Type = Domain.Entities.Persona.Type;

namespace API.Dtos
{
    public class BsPersonaDto
    {
         public string Nif { get; set;}
        public string Nombre { get; set;}
        public string Apellido1 { get; set;}
        public string Apellido2 { get; set;}
        public string Ciudad { get; set;}
        public string Direccion { get; set;}
        public string Telefono { get; set;}
        public DateTime Fecha_nacimiento {get;set;}
        public Genero Sexo {get;set;}
        public Type Tipo {get;set;}
    }
    public class PointUno
    {
        public string Apellido1 { get; set;}
        public string Apellido2 { get; set;}
        public string Nombre { get; set;}

    }
    public class AlumnoAsignaturaDto
    {
        public string Nombre { get;}
        public List<NombreAsignaturaDto> Asignaturas {get;set;} 
        public int Anio_fin {get;set;}

    }
}