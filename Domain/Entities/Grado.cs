namespace Domain.Entities
{
    public class Grado : BaseEntity
    {
        public string Nombre { get; set; }
        public ICollection<Asignatura> Asignaturas {get;set;}

    }
}