namespace Domain.Entities
{
    public class Profesor
    {
        public int Id_profesor {get;set;}
        public int Id_departamento {get;set;}
        public Departamento Departamento {get;set;}
        public int Id_persona {get;set;}
        public Persona Persona {get;set;}
        public ICollection<Asignatura> Asignaturas {get;set;}
        
    }
}