namespace Domain.Entities
{
    public class TipoAsignatura: BaseEntity
    {
        public string Descripcion {get;set;}
        public ICollection<Asignatura> Asignaturas {get;set;}
    }
}