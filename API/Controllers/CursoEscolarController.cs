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
public class CursoEscolarController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CursoEscolarController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("PointVeintiCuatro")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Object>>> PointVeintiCinco()
    {
        var cursosEscolares = await _unitOfWork.CursosEscolares.GetCantAlumnosMatrEnCurso();
        return Ok(cursosEscolares);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<BsCursoEscolarDto>>> Get()
    {
        var cursosEscolares = await _unitOfWork.CursosEscolares.GetAllAsync();
        return _mapper.Map<List<BsCursoEscolarDto>>(cursosEscolares);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(CursoEscolar cursoEscolar)
    {
        var cursoEscolarPost = _mapper.Map<CursoEscolar>(cursoEscolar);
        _unitOfWork.CursosEscolares.Add(cursoEscolarPost);
        await _unitOfWork.SaveAsync();
        if (cursoEscolar == null)
        {
            return BadRequest();
        }
        return "CursoEscolar Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] CursoEscolar cursoEscolar)
    {
        if (cursoEscolar == null|| id != cursoEscolar.Id)
        {
            return BadRequest();
        }
        var existe = await _unitOfWork.CursosEscolares.GetByIdAsync(id);

        if (existe == null)
        {
            return NotFound();
        }
        _mapper.Map(cursoEscolar, existe);
        _unitOfWork.CursosEscolares.Update(existe);
        await _unitOfWork.SaveAsync();

        return "CursoEscolar Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var cursoEscolar = await _unitOfWork.CursosEscolares.GetByIdAsync(id);
        if (cursoEscolar == null)
        {
            return NotFound();
        }
        _unitOfWork.CursosEscolares.Remove(cursoEscolar);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}