namespace API.Dtos
{
    public class CursoMatriculaDto
    {
        public BsMatriculaDto Alumno {get;set;}
        public BsAsignaturaDto Asignatura {get;set;}
    }

    public class BsMatriculaDto
    {
        public BsPersonaDto Alumno {get;set;}
        public BsAsignaturaDto Asignatura {get;set;}
        public MatriculaCursoEscolarDto CursoEscolar {get;set;}

    }
}