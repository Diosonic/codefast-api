using Codefast.Models;
using Codefast.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Codefast.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RodadaController : ControllerBase
    {
        private readonly IRodadaRepository _repository;

        public RodadaController(IRodadaRepository repository)
        {
            _repository = repository;
        }


        [HttpPost("{idTorneio}/cria-rodadas")]
        public async Task<ActionResult<IEnumerable<Rodada>>> Post(int idTorneio)
        {
            var rodadasEmAndamento = await _repository.SelecionaRodadaEmAndamento(idTorneio);

            if (rodadasEmAndamento.Any())
                return BadRequest("Já existe uma rodada em andamento.");

            _repository.CriaRodadasEtapaMataMata(idTorneio);

            return Ok("Rodadas criadas com sucesso.");
        }

        [HttpGet("{idTorneio}/rodada-andamento")]
        public async Task<ActionResult<IEnumerable<Rodada>>> Get(int idTorneio)
        {
            IEnumerable<Rodada> rodadas = await _repository.SelecionaRodadaEmAndamento(idTorneio);

            return Ok(rodadas);
        }
    }
}
