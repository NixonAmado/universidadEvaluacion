1. Devuelve un listado con el primer apellido, segundo apellido y el nombre de todos los alumnos. El listado deberá estar ordenado alfabéticamente de menor a mayor por el primer apellido, segundo apellido y nombre.
    ```    
    RUTA= api/Persona/GetPointUno
    ```

    LOGICA
    ```
        return await _context.Personas
                    .Where(p => p.Tipo == Persona.Type.alumno)
                    .OrderBy(p => p.Apellido1)
                    .ThenBy(p => p.Apellido2)
                    .ThenBy(p => p.Nombre)
                    .ToListAsync();
        
    ```

2. Averigua el nombre y los dos apellidos de los alumnos que **no** han dado de alta su número de teléfono en la base de datos.
    ```    
    RUTA= api/Persona/GetPointDos
    ```

    LOGICA
    ```
        return await _context.Personas
                .Where(p => p.Tipo == Persona.Type.alumno && !string.IsNullOrEmpty(p.Telefono) )
                .ToListAsync();
    ```

3. Devuelve el listado de los alumnos que nacieron en `1999`.
   ```    
    RUTA= api/Persona/GetPointTres/{year}
    ```

    LOGICA
    ```
        return await _context.Personas
                    .Where(p => p.Tipo == Persona.Type.alumno && p.Fecha_nacimiento.Year == year)
                    .ToListAsync();  
    ```

4. Devuelve el listado de `profesores` que **no** han dado de alta su número de teléfono en la base de datos y además su nif termina en `K`.
    ```
    RUTA= api/Profesor/PointCuatro/{letter}
    ```
    LOGICA
    ```
        return await _context.Personas
                    .Where(p => p.Tipo == Persona.Type.profesor &&
                    !string.IsNullOrEmpty(p.Telefono) && 
                    p.Nif.EndsWith(letter))
                    .ToListAsync();```

5. Devuelve el listado de las asignaturas que se imparten en el primer cuatrimestre, en el tercer curso del grado que tiene el identificador `7`.
    ```
    RUTA= /api/asignatura/PointCinco/1/2/7
    ```
    LOGICA
    ```
        return await _context.Asignaturas
                    .Where(p => p.Cuatrimestre == cuatr && p.Curso == curso && p.Id_Grado == grado).ToListAsync();
    ```

6. Devuelve un listado con los datos de todas las **alumnas** que se han matriculado alguna vez en el `Grado en Ingeniería Informática (Plan 2015)`.
    ```
    RUTA= /api/persona/PointSeis
    ```
    LOGICA
    ```
        return await _context.Personas
                    .Where(p => p.Tipo == Persona.Type.alumno && 
                    p.Sexo == Persona.Genero.M &&
                    p.Matriculas.Any(m => m.Asignatura.Grado.Nombre == "Grado en Ingeniería Informática (Plan 2015)"))
                    .ToListAsync();
    ```

7. Devuelve un listado con todas las asignaturas ofertadas en el `Grado en Ingeniería Informática (Plan 2015)`.
    ```
    RUTA= api/Asignatura/PointSiete
    ```
    LOGICA
    ```
        return await _context.Asignaturas
                .Where(p => p.Grado.Nombre == "Grado en Ingeniería Informática (Plan 2015)") 
                .ToListAsync();
    ```

8. Devuelve un listado de los `profesores` junto con el nombre del `departamento` al que están vinculados. El listado debe devolver cuatro columnas, `primer apellido, segundo apellido, nombre y nombre del departamento.` El resultado estará ordenado alfabéticamente de menor a mayor por los `apellidos y el nombre.`
    ```
    RUTA= /api/profesor/PointOcho
    ```
    LOGICA
    ```
        return await _context.Profesores
                .Include(p => p.Persona)
                .Include(p => p.Departamento)
                .OrderBy(p => p.Persona.Apellido1)
                .ThenBy(p => p.Persona.Apellido2)
                .ThenBy(p => p.Persona.Nombre)
                .ToListAsync();
    ```
    

9. Devuelve un listado con el nombre de las asignaturas, año de inicio y año de fin del curso escolar del alumno con nif `26902806M`.
    ```
    RUTA= /api/Persona/PointNueve/{26902806M}
    ```
    LOGICA
    ```
        return await _context.Personas     
                .Where(p => p.Tipo == Persona.Type.alumno && p.Nif.ToUpper() == nif.ToUpper())
                .Select(p => new AlumnoAsignatura
                {
                    Nombre = p.Nombre,
                    Asignaturas = p.Matriculas.Where(p => p.Id_alumno == p.Id_alumno).Select(p => p.Asignatura).ToList(),
                    Anio_fin = p.Matriculas.GroupBy(p => p.Id_alumno).Select(group => group.FirstOrDefault().CursoEscolar.Anio_fin).FirstOrDefault
                })
                .ToListAsync();
    ```

10. Devuelve un listado con el nombre de todos los departamentos que tienen profesores que imparten alguna asignatura en el `Grado en Ingeniería Informática (Plan 2015)`.
    ```
    RUTA= /api/Departamento/PointDiez
    ```
    LOGICA
    ```
    return await _context.Departamentos
                .Include(P=> P.Profesores)
                .Where(d => d.Profesores.Any(p => p.Asignaturas.Any(p => p.Grado.Nombre == "Grado en Ingeniería Informática (Plan 2015)")))
                .ToListAsync();
     ```

11. Devuelve un listado con todos los alumnos que se han matriculado en alguna asignatura durante el curso escolar 2018/2019.
    ```
    RUTA= /api/Persona/PointOnce
    ```
    LOGICA
     ```
        return await _context.Personas
                .Where(p => p.Matriculas.Any(m => m.Id_alumno == p.Id) &&
                 p.Matriculas.Any(p => 
                 p.CursoEscolar.Anio_inicio == 2018 &&
                 p.CursoEscolar.Anio_fin == 2019 )).Distinct()
                 .ToListAsync();
     ```

12. Devuelve un listado con los nombres de **todos** los profesores y los departamentos que tienen vinculados. El listado también debe mostrar aquellos profesores que no tienen ningún departamento asociado. El listado debe devolver cuatro columnas, nombre del departamento, primer apellido, segundo apellido y nombre del profesor. El resultado estará ordenado alfabéticamente de menor a mayor por el nombre del departamento, apellidos y el nombre.
    ```
    RUTA= /api/profesor/PointDoce
    ```
    LOGICA
    ```
        return await _context.Profesores
                        .Include(p => p.Departamento)
                        .Include(p => p.Persona)
                        .OrderBy(p => p.Departamento)
                        .ThenBy(p => p.Persona.Apellido1)
                        .ThenBy(p => p.Persona.Apellido2)
                        .ThenBy(p => p.Persona.Nombre)
                        .ToListAsync();
     ```

13. Devuelve un listado con los profesores que no están asociados a un departamento.Devuelve un listado con los departamentos que no tienen profesores asociados.
    ```
    RUTA= /api/profesor/PointTrece
    ```
    LOGICA
    ```
     public async Task<(IEnumerable<Profesor> profesores ,IEnumerable<Departamento> departamentos)> GetProfesoresYDepartamentos()
        {
            var profesores =  await _context.Profesores
                                .Include(p => p.Persona)
                                .Include(p => p.Departamento)
                                .Where(p => p.Departamento == null)
                                .ToListAsync();

            var departamentos =  await _context.Departamentos
                                .Where(p => p.Profesores.Count() == 0)
                                .ToListAsync();   

            return (profesores, departamentos);}
    ```

14. Devuelve un listado con los profesores que no imparten ninguna asignatura.
    ```
    RUTA= /api/profesor/pointCatorce
    ```
    LOGICA
     ```
     return await _context.Profesores
                    .Where(p => !p.Asignaturas.Any())
                    .ToListAsync();
     ```

15. Devuelve un listado con las asignaturas que no tienen un profesor asignado.
    ```
    /api/Asignatura/PointQuince
    ```
     LOGICA
     ```
     return await _context.Asignaturas
                    .Where(p =>  p.Id_profesor == null)
                    .ToListAsync();
     ```

16. Devuelve un listado con todos los departamentos que tienen alguna asignatura que no se haya impartido en ningún curso escolar. El resultado debe mostrar el nombre del departamento y el nombre de la asignatura que no se haya impartido nunca.
     ```
    /api/Asignatura/PointQuince
    ```
     LOGICA
     ```
     return await _context.Asignaturas
                    .Where(p =>  p.Id_profesor == null)
                    .ToListAsync();
     ```

17. Devuelve el número total de **alumnas** que hay.

     ```sql
       # Consulta Aqui
     ```

18. Calcula cuántos alumnos nacieron en `1999`.

     ```sql
       # Consulta Aqui
     ```

19. Calcula cuántos profesores hay en cada departamento. El resultado sólo debe mostrar dos columnas, una con el nombre del departamento y otra con el número de profesores que hay en ese departamento. El resultado sólo debe incluir los departamentos que tienen profesores asociados y deberá estar ordenado de mayor a menor por el número de profesores.

     ```sql
       # Consulta Aqui
     ```

20. Devuelve un listado con todos los departamentos y el número de profesores que hay en cada uno de ellos. Tenga en cuenta que pueden existir departamentos que no tienen profesores asociados. Estos departamentos también tienen que aparecer en el listado.

     ```sql
       # Consulta Aqui
     ```

21. Devuelve un listado con el nombre de todos los grados existentes en la base de datos y el número de asignaturas que tiene cada uno. Tenga en cuenta que pueden existir grados que no tienen asignaturas asociadas. Estos grados también tienen que aparecer en el listado. El resultado deberá estar ordenado de mayor a menor por el número de asignaturas.

     ```sql
       # Consulta Aqui
     ```

22. Devuelve un listado con el nombre de todos los grados existentes en la base de datos y el número de asignaturas que tiene cada uno, de los grados que tengan más de `40` asignaturas asociadas.

     ```sql
       # Consulta Aqui
     ```

23. Devuelve un listado que muestre el nombre de los grados y la suma del número total de créditos que hay para cada tipo de asignatura. El resultado debe tener tres columnas: nombre del grado, tipo de asignatura y la suma de los créditos de todas las asignaturas que hay de ese tipo. Ordene el resultado de mayor a menor por el número total de crédidos.

     ```sql
       # Consulta Aqui
     ```

24. Devuelve un listado que muestre cuántos alumnos se han matriculado de alguna asignatura en cada uno de los cursos escolares. El resultado deberá mostrar dos columnas, una columna con el año de inicio del curso escolar y otra con el número de alumnos matriculados.

     ```sql
       # Consulta Aqui
     ```

25. Devuelve un listado con el número de asignaturas que imparte cada profesor. El listado debe tener en cuenta aquellos profesores que no imparten ninguna asignatura. El resultado mostrará cinco columnas: id, nombre, primer apellido, segundo apellido y número de asignaturas. El resultado estará ordenado de mayor a menor por el número de asignaturas.

     ```sql
       # Consulta Aqui
     ```

26. Devuelve todos los datos del alumno más joven.

     ```sql
       # Consulta Aqui
     ```

27. Devuelve un listado con los profesores que no están asociados a un departamento.

     ```sql
       # Consulta Aqui
     ```

28. Devuelve un listado con los departamentos que no tienen profesores asociados.

     ```sql
       # Consulta Aqui
     ```

29. Devuelve un listado con los profesores que tienen un departamento asociado y que no imparten ninguna asignatura.

     ```sql
       # Consulta Aqui
     ```

30. Devuelve un listado con las asignaturas que no tienen un profesor asignado.

     ```sql
       # Consulta Aqui
     ```

31. Devuelve un listado con todos los departamentos que no han impartido asignaturas en ningún curso escolar.

     ```sql
       # Consulta Aqui
     ```