namespace Domain.Entities
{
    public class Departamento : BaseEntity
    {
        public string Nombre {get;set;}
        public ICollection<Profesor> Profesores {get;set;}
    }
}