using Codefast.Models;
using Codefast.Repository;
using Codefast.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Codefast.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RodadaController : ControllerBase
    {
        private readonly IRodadaRepository _repository;
        private readonly IControleMataMataRepository _controleMataMataRepository;

        public RodadaController(IRodadaRepository repository, IControleMataMataRepository controleMataMataRepository)
        {
            _repository = repository;
            _controleMataMataRepository = controleMataMataRepository;
        }


        [HttpPost("{idTorneio}/cria-rodadas")]
        public async Task<ActionResult<IEnumerable<Rodada>>> Post(int idTorneio)
        {
            var rodadasEmAndamento = await _repository.SelecionaRodadaEmAndamento(idTorneio);

            if (rodadasEmAndamento.Any())
                return BadRequest("Já existe uma rodada em andamento.");

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
