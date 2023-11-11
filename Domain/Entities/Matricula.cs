namespace Domain.Entities
{
    public class Matricula
    {
        public int Id_alumno {get;set;}
        public Persona Alumno {get;set;}
        public int Id_Asignatura {get;set;}
        public Asignatura Asignatura {get;set;}
        public int Id_cursoEscolar {get;set;}
        public CursoEscolar CursoEscolar {get;set;}
                        
    }
}