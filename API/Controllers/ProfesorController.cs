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
public class ProfesorController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProfesorController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    [HttpGet("PointCuatro/{letter}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<BsPersonaDto>>> GetPointCuatro(string letter)
    {
        var profesores = await _unitOfWork.Profesores.GetProfesoresConNumero(letter);
        return _mapper.Map<List<BsPersonaDto>>(profesores);
    }

    [HttpGet("PointOcho")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<DepartamentoProfesorDto>>> GetPointOcho()
    {
        var profesores = await _unitOfWork.Profesores.GetProfesoresDepartamento();
        return _mapper.Map<List<DepartamentoProfesorDto>>(profesores);
    }
    [HttpGet("PointDoce")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<DepartamentoProfesorDto>>> GetPointDoce()
    {
        var profesores = await _unitOfWork.Profesores.GetAllProfesores();
        return _mapper.Map<List<DepartamentoProfesorDto>>(profesores);
    }
    [HttpGet("PointTrece")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<(IEnumerable<Object>,IEnumerable<Object>)>> GetPointTrece()
    {
        var profesores = await _unitOfWork.Profesores.GetAllProfesores();
        return Ok(profesores);
    }
    [HttpGet("PointCatorce")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<DepartamentoProfesorDto>>> GetPointCatorce()
    {
        var profesores = await _unitOfWork.Profesores.GetProfesoresNoAsignatura();
        return _mapper.Map<List<DepartamentoProfesorDto>>(profesores);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<BsProfesorDto>>> Get()
    {
        var Profesores = await _unitOfWork.Profesores.GetAllAsync();
        return _mapper.Map<List<BsProfesorDto>>(Profesores);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Profesor Profesor)
    {
        var ProfesorPost = _mapper.Map<Profesor>(Profesor);
        _unitOfWork.Profesores.Add(ProfesorPost);
        await _unitOfWork.SaveAsync();
        if (Profesor == null)
        {
            return BadRequest();
        }
        return "Profesor Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Profesor Profesor)
    {
        if (Profesor == null|| id != Profesor.Id_profesor)
        {
            return BadRequest();
        }
        var existe = await _unitOfWork.Profesores.GetByIdAsync(id);

        if (existe == null)
        {
            return NotFound();
        }
        _mapper.Map(Profesor, existe);
        _unitOfWork.Profesores.Update(existe);
        await _unitOfWork.SaveAsync();

        return "Profesor Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Profesor = await _unitOfWork.Profesores.GetByIdAsync(id);
        if (Profesor == null)
        {
            return NotFound();
        }
        _unitOfWork.Profesores.Remove(Profesor);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}