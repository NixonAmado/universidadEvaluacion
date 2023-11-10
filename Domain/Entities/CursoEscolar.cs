using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CursoEscolar: BaseEntity
    {
        public DateOnly Anio_inicio {get;set;}
        public DateOnly Anio_fin {get;set;}
        public ICollection<Matricula> Matriculas {get;set;}
    }
}