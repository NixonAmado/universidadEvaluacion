namespace Domain.Entities
{
    public class Persona :BaseEntity
    {
        public int Nif { get; set;}
        public string Nombre { get; set;}
        public string Apellido1 { get; set;}
        public string Apellido2 { get; set;}
        public string Ciudad { get; set;}
        public string Direccion { get; set;}
        public string Telefono { get; set;}
        public DateTime Fecha_nacimiento {get;set;}
        public enum Genero {H,M}
        public Genero Sexo {get;set;}
        public enum Type{profesor,alumno}
        public Type Tipo {get;set;}
        public ICollection<Profesor> Profesores {get;set;}
        public ICollection<Matricula> Matriculas {get;set;}

    }


}