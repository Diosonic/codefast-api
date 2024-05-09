using Codefast.Models;
using Codefast.Models.DTOs.ControleMataMata;
using Codefast.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Codefast.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SementeRodadaController : ControllerBase
    {
        private readonly ISementeRodadaRepository _repository;

        public SementeRodadaController(ISementeRodadaRepository repository)
        {
            _repository = repository;
        }



        [HttpPost("{idTorneio}/cria-chaves")]
        public async Task<ActionResult<SementeRodada>> CriaChavesMataMata(int idTorneio, AdicionarChavesDTO request)
        {
            SementeRodada equipe = await _repository.GetSementeRodadaByIdAsync(idTorneio, request);

            //if (equipes is null)
            //    return BadRequest("Não existe equipes para criar a rodada");


            //var grupos = equipes.Select((equipe, index) => new { Index = index, Equipe = equipe })
            //        .GroupBy(x => x.Index / 2)
            //        .Select(grupo => grupo.Select(x => x.Equipe).ToList())
            //        .ToList();




            return Ok();
        }
    }
}
