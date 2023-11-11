namespace Domain.Entities
{
    public class AlumnoAsignatura : Persona
    {
        public ICollection<Asignatura> Asignaturas {get;set;} 
        public int Anio_fin {get;set;}
        
    }
}