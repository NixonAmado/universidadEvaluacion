namespace API.Dtos
{
    public class DepartamentoProfesorDto
    {
        public ProfesorDepartamentoDto Departamento {get;set;}
        public PointUno Persona {get;set;}

    }
    public class BsProfesorDto
    {
        public BsDepartamentoDto Departamento {get;set;}
        public BsPersonaDto Persona {get;set;}
        public List<BsAsignaturaDto> Asignaturas {get;set;}

    }


}