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
public class AsignaturaController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    
    public AsignaturaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    [HttpGet("PointCinco/{cuatr}/{curso}/{grado}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<BsAsignaturaDto>>> GetPointCinco(int cuatr,int curso,int grado)
    {
        var asignaturas = await _unitOfWork.Asignaturas.GetAsignaturasPorGrado(cuatr,curso,grado);
        return _mapper.Map<List<BsAsignaturaDto>>(asignaturas);
    }
    [HttpGet("PointSiete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<BsAsignaturaDto>>> PointSiete()
    {
        var asignaturas = await _unitOfWork.Asignaturas.GetAsignaturasOfertadasGrado();
        return _mapper.Map<List<BsAsignaturaDto>>(asignaturas);
    }
    [HttpGet("PointQuince")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<BsAsignaturaDto>>> PointQuince()
    {
        var asignaturas = await _unitOfWork.Asignaturas.GetAsignaturasSinProfesor();
        return _mapper.Map<List<BsAsignaturaDto>>(asignaturas);
    }
    [HttpGet("PointTreinta")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<BsAsignaturaDto>>> PointTreinta()
    {
        var asignaturas = await _unitOfWork.Asignaturas.GetAsignaturaSinProfesor();
        return _mapper.Map<List<BsAsignaturaDto>>(asignaturas);
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<BsAsignaturaDto>>> Get()
    {
        var asignaturas = await _unitOfWork.Asignaturas.GetAllAsync();
        return _mapper.Map<List<BsAsignaturaDto>>(asignaturas);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Asignatura asignatura)
    {
        var asignaturaPost = _mapper.Map<Asignatura>(asignatura);
        _unitOfWork.Asignaturas.Add(asignaturaPost);
        await _unitOfWork.SaveAsync();
        if (asignatura == null)
        {
            return BadRequest();
        }
        return "Asignatura Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Asignatura asignatura)
    {
        if (asignatura == null|| id != asignatura.Id)
        {
            return BadRequest();
        }
        var existe = await _unitOfWork.Asignaturas.GetByIdAsync(id);

        if (existe == null)
        {
            return NotFound();
        }
        _mapper.Map(asignatura, existe);
        _unitOfWork.Asignaturas.Update(existe);
        await _unitOfWork.SaveAsync();

        return "Asignatura Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var asignatura = await _unitOfWork.Asignaturas.GetByIdAsync(id);
        if (asignatura == null)
        {
            return NotFound();
        }
        _unitOfWork.Asignaturas.Remove(asignatura);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}