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
public class DepartamentoController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DepartamentoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    [HttpGet("PointDiez")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProfesorDepartamentoDto>>>PointDiez()
    {
        var Departamentos = await _unitOfWork.Departamentos.GetDepartamentosPorProfesores();
        return _mapper.Map<List<ProfesorDepartamentoDto>>(Departamentos);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<BsDepartamentoDto>>> Get()
    {
        var Departamentos = await _unitOfWork.Departamentos.GetAllAsync();
        return _mapper.Map<List<BsDepartamentoDto>>(Departamentos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Departamento Departamento)
    {
        var DepartamentoPost = _mapper.Map<Departamento>(Departamento);
        _unitOfWork.Departamentos.Add(DepartamentoPost);
        await _unitOfWork.SaveAsync();
        if (Departamento == null)
        {
            return BadRequest();
        }
        return "Departamento Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Departamento Departamento)
    {
        if (Departamento == null|| id != Departamento.Id)
        {
            return BadRequest();
        }
        var existe = await _unitOfWork.Departamentos.GetByIdAsync(id);

        if (existe == null)
        {
            return NotFound();
        }
        _mapper.Map(Departamento, existe);
        _unitOfWork.Departamentos.Update(existe);
        await _unitOfWork.SaveAsync();

        return "Departamento Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Departamento = await _unitOfWork.Departamentos.GetByIdAsync(id);
        if (Departamento == null)
        {
            return NotFound();
        }
        _unitOfWork.Departamentos.Remove(Departamento);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}