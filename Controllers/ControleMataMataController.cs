using Codefast.Models;
using Codefast.Models.DTOs.ControleMataMata;
using Codefast.Models.DTOs.Equipe;
using Codefast.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Codefast.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ControleMataMataController : ControllerBase
    {
        private readonly IControleMataMataRepository _repository;

        public ControleMataMataController(IControleMataMataRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{idTorneio}")]
        public async Task<ActionResult<IEnumerable<ControleMataMataDTO>>> Get(int idTorneio)
        {

            IEnumerable<ControleMataMataDTO> controleMataMata = await _repository.GetControleMataMataAsync(idTorneio);

            if (controleMataMata == null)
                return NotFound("Nenhuma equipe foi encontrada para a etapa mata-mata");

            if (controleMataMata.Count() != 8)
            {
                return BadRequest("Ops! Quantidade das equipes tem que ser igual a 8 para para começar a etapa de mata-mata.");
            }

            return Ok(controleMataMata);
        }

        [HttpGet("{id}/equipes")]
        public async Task<ActionResult<ControleMataMata>> GetById(int id)
        {
            ControleMataMata controleMataMata = await _repository.GetControleMataMataByIdAsync(id);

            if (controleMataMata == null)
                return NotFound("Nenhuma equipe foi encontrada para essa etapa mata-mata");

            return Ok(controleMataMata);
        }

        [HttpGet("{idTorneio}/equipes/validando")]
        public async Task<ActionResult<IEnumerable<ControleMataMataDTO>>> SelecionaEquipesEmValidacao(int idTorneio)
        {
            IEnumerable<ControleMataMataDTO> controleMataMata = await _repository.GetControleMataMataEmValidacaoAsync(idTorneio);

            if (controleMataMata == null)
                return NotFound("Nenhuma equipe foi encontrada para essa etapa mata-mata");

            return Ok(controleMataMata);
        }

        [HttpPut("{id}/alterar-status-validacao")]
        public async Task<ActionResult<ControleMataMata>> AlteraStatusValidacao(int id, AtualizaControleMataMataDTO request)
        {
            if (request == null)
                return BadRequest("Dados inválidos");

            ControleMataMata controleExistente = await _repository.GetControleMataMataByIdAsync(id);

            if (controleExistente == null)
               return NotFound("Controle não encontrado");

            if (request.StatusValidacao != null)
                controleExistente.StatusValidacao = request.StatusValidacao;

            await _repository.UpdateAsync(controleExistente);

            return Ok(controleExistente.StatusValidacao);
        }


    }
}
