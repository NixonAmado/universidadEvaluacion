namespace Domain.Entities
{
    public class Asignatura : BaseEntity
    {
        public string Nombre {get;set;}
        public float Creditos {get;set;}
        public int Curso {get;set;}
        public int Cuatrimestre {get;set;}

        public int? Id_profesor {get;set;}
        public Profesor Profesor {get;set;}
        public int? Id_TipoAsignatura {get;set;}
        public TipoAsignatura TipoAsignatura {get;set;}
        public int Id_Grado {get;set;}
        public Grado Grado {get;set;}
        public enum AsignaturaType {básica,obligatoria,optativa}
        public AsignaturaType Tipo{get;set;}
        public ICollection<Matricula> Matriculas {get;set;}

        
    }
}