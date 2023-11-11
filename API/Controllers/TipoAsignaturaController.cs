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
public class TipoAsignaturaController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TipoAsignaturaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<BsTipoAsignaturaDto>>> Get()
    {
        var TiposAsignaturas = await _unitOfWork.TiposAsignaturas.GetAllAsync();
        return _mapper.Map<List<BsTipoAsignaturaDto>>(TiposAsignaturas);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(TipoAsignatura TipoAsignatura)
    {
        var TipoAsignaturaPost = _mapper.Map<TipoAsignatura>(TipoAsignatura);
        _unitOfWork.TiposAsignaturas.Add(TipoAsignaturaPost);
        await _unitOfWork.SaveAsync();
        if (TipoAsignatura == null)
        {
            return BadRequest();
        }
        return "TipoAsignatura Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] TipoAsignatura TipoAsignatura)
    {
        if (TipoAsignatura == null|| id != TipoAsignatura.Id)
        {
            return BadRequest();
        }
        var existe = await _unitOfWork.TiposAsignaturas.GetByIdAsync(id);

        if (existe == null)
        {
            return NotFound();
        }
        _mapper.Map(TipoAsignatura, existe);
        _unitOfWork.TiposAsignaturas.Update(existe);
        await _unitOfWork.SaveAsync();

        return "TipoAsignatura Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var TipoAsignatura = await _unitOfWork.TiposAsignaturas.GetByIdAsync(id);
        if (TipoAsignatura == null)
        {
            return NotFound();
        }
        _unitOfWork.TiposAsignaturas.Remove(TipoAsignatura);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}