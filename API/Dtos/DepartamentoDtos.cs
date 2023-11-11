namespace API.Dtos
{
    public class ProfesorDepartamentoDto
    {
        public string Nombre {get;set;}
    }

    public class BsDepartamentoDto
    {
        public string Nombre {get;set;}
        public List<DepartamentoProfesorDto> Profesores {get;set;}

    }
}