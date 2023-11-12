using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
// [ApiVersion("1.0")]
// [ApiVersion("1.1")]
// [Authorize(Roles = "Empleado, Administrador, Gerente")]
public class PersonaController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PersonaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    

    [HttpGet("GetPointUno")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<PointUno>>> GetPointUno()
    {
        var Alumnos = await _unitOfWork.Personas.GetAlumnosOrdendos();
    return _mapper.Map<List<PointUno>>(Alumnos);
    }
    [HttpGet("GetPointDos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<PointUno>>> GetPointDos()
    {
        var Alumnos = await _unitOfWork.Personas.GetAlumnosConNumero();
    return _mapper.Map<List<PointUno>>(Alumnos);
    }

    [HttpGet("GetPointTres/{year}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<PointUno>>> GetPointTres(int year)
    {
        var Alumnos = await _unitOfWork.Personas.GetAlumnosNacidosEnX(year);
    return _mapper.Map<List<PointUno>>(Alumnos);
    }
    [HttpGet("PointSeis")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<BsPersonaDto>>> GetPointSeis()
    {
        var Alumnas = await _unitOfWork.Personas.GetAlumnasMatriculadas();
        return _mapper.Map<List<BsPersonaDto>>(Alumnas);
    }
    [HttpGet("PointNueve/{nif}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<AlumnoAsignaturaDto>>> GetPointNueve(string nif)
    {
        var Alumnos = await _unitOfWork.Personas.GetAsignaturasPorAlumno(nif);
        return _mapper.Map<List<AlumnoAsignaturaDto>>(Alumnos);
    }
    [HttpGet("PointOnce")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<BsPersonaDto>>> PointOnce()
    {
        var Alumnos = await _unitOfWork.Personas.GetAlumnosMatriculados();
        return _mapper.Map<List<BsPersonaDto>>(Alumnos);
    }
    [HttpGet("PointDiezSiete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Object>>> PointDiezSiete()
    {
        var Alumnas = await _unitOfWork.Personas.GetCantAlumnas();
        return Ok(Alumnas);
    }
    [HttpGet("PointDiezOcho/{year}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Object> PointDiezOcho(int year)
    {
        var Alumnas = await _unitOfWork.Personas.GetCantAlumnosEnFecha(year);
        return Ok(Alumnas);
    }
    [HttpGet("PointVeintiSeis")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<BsPersonaDto> PointVeintiSeis()
    {
        var Alumnos = await _unitOfWork.Personas.GetAlumnoMasJoven();
        return _mapper.Map<BsPersonaDto>(Alumnos);
    }
        

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<BsPersonaDto>>> Get()
    {
        var Personas = await _unitOfWork.Personas.GetAllAsync();
        return _mapper.Map<List<BsPersonaDto>>(Personas);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Persona Persona)
    {
        var PersonaPost = _mapper.Map<Persona>(Persona);
        _unitOfWork.Personas.Add(PersonaPost);
        await _unitOfWork.SaveAsync();
        if (Persona == null)
        {
            return BadRequest();
        }
        return "Persona Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Persona Persona)
    {
        if (Persona == null|| id != Persona.Id)
        {
            return BadRequest();
        }
        var existe = await _unitOfWork.Personas.GetByIdAsync(id);

        if (existe == null)
        {
            return NotFound();
        }
        _mapper.Map(Persona, existe);
        _unitOfWork.Personas.Update(existe);
        await _unitOfWork.SaveAsync();

        return "Persona Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Persona = await _unitOfWork.Personas.GetByIdAsync(id);
        if (Persona == null)
        {
            return NotFound();
        }
        _unitOfWork.Personas.Remove(Persona);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}