using Codefast.Models;
using Codefast.Models.DTOs.Torneio;
using Codefast.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Utils;

namespace Codefast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TorneioController : ControllerBase
    {
        //private readonly IBaseRepository<Torneio> _repository;

        //public TorneioController(IBaseRepository<Torneio> repository)
        //{
        //    _repository = repository;
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var torneios = await _repository.GetAllAsync();

        //    if (torneios is null)
        //        return NotFound("Torneios não encontrados");

        //    var torneiosDTO = torneios.Select(torneio => new TorneioDTO {
        //        Id = torneio.Id,
        //        Titulo = torneio.Titulo
        //    }).ToList();

        //    return Ok(torneiosDTO);
        //}

        //[HttpGet("id")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var torneio = await _repository.GetByIdAsync(id).;

        //    if (torneio is null)
        //        return NotFound("Torneio não encontrado");

        //    var torneioDTO = new TorneioDetalhesDTO
        //    {
        //        Id = torneio.Id,
        //        Titulo = torneio.Titulo,
        //        Equipes = torneio.Equipes
        //    };

        //    return Ok(torneioDTO);
        //}

    }
}
