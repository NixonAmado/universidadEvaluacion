namespace Domain.Entities
{
    public class CursoEscolar: BaseEntity
    {
        public int Anio_inicio {get;set;}
        public int Anio_fin {get;set;}
        public ICollection<Matricula> Matriculas {get;set;}
    }
}