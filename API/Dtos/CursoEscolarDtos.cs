namespace API.Dtos
{
    public class BsCursoEscolarDto
    {
        public DateOnly Anio_inicio {get;set;}
        public DateOnly Anio_fin {get;set;}
        public List<CursoMatriculaDto> Matriculas {get;set;}
    }
    public class MatriculaCursoEscolarDto
    {
        public DateOnly Anio_inicio {get;set;}
        public DateOnly Anio_fin {get;set;}
    }
}