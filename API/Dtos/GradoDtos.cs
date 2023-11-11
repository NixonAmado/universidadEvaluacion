using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class BsGradoDto
    {
        public string Nombre { get; set; }
        public List<BsAsignaturaDto> Asignaturas {get;set;}

    }
}