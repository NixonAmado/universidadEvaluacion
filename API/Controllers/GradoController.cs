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
public class GradoController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GradoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet("PointVeintiUno")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Object>>> PointVeintiUno()
    {
        var Grados = await _unitOfWork.Grados.GetCantAsigNoAsosiadaPorGrado();
        return Ok(Grados);
    }

    [HttpGet("PointVeintiDos/{cantidadMin}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Object>>> PointVeintiDos(int cantidadMin)
    {
        var Grados = await _unitOfWork.Grados.GetCantAsigPorcantidadEnGrado(cantidadMin);
        return Ok(Grados);
    }
    
    [HttpGet("PointVeintiTres")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Object>>> PointVeintiTres()
    {
        var Grados = await _unitOfWork.Grados.GetGradosSumCreditos();
        return Ok(Grados);
    }
       
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<BsGradoDto>>> Get()
    {
        var Grados = await _unitOfWork.Grados.GetAllAsync();
        return _mapper.Map<List<BsGradoDto>>(Grados);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Grado Grado)
    {
        var GradoPost = _mapper.Map<Grado>(Grado);
        _unitOfWork.Grados.Add(GradoPost);
        await _unitOfWork.SaveAsync();
        if (Grado == null)
        {
            return BadRequest();
        }
        return "Grado Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Grado Grado)
    {
        if (Grado == null|| id != Grado.Id)
        {
            return BadRequest();
        }
        var existe = await _unitOfWork.Grados.GetByIdAsync(id);

        if (existe == null)
        {
            return NotFound();
        }
        _mapper.Map(Grado, existe);
        _unitOfWork.Grados.Update(existe);
        await _unitOfWork.SaveAsync();

        return "Grado Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
//[Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Grado = await _unitOfWork.Grados.GetByIdAsync(id);
        if (Grado == null)
        {
            return NotFound();
        }
        _unitOfWork.Grados.Remove(Grado);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}
