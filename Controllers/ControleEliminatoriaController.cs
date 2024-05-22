using Codefast.Models;
using Codefast.Models.DTOs.ControleEliminatoria;
using Codefast.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Codefast.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ControleEliminatoriaController : ControllerBase
    {
        private readonly IControleEliminatoriaRepository _repository;

        public ControleEliminatoriaController(IControleEliminatoriaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ControleEliminatoriaDTO>>> GetByidAsync(int id)
        {
            ControleEliminatoriaDTO equipeExistente = await _repository.GetControleEliminatoriaByIdAsyncList(id);

            return Ok(equipeExistente);
        }

        [HttpGet("{id}/equipes")]
        public async Task<ActionResult<IEnumerable<ControleEliminatoriaDTO>>> SelecionaEquipesCredenciadas(int id)
        {
            IEnumerable<ControleEliminatoriaDTO> equipes = await _repository.GetEquipesCredenciadas(id);

            if (equipes == null)
                return NotFound("Nenhuma equipe foi encontrada para essa etapa eliminatória");

            return Ok(equipes);
        }

        [HttpGet("{id}/equipes/validando")]
        public async Task<ActionResult<IEnumerable<ControleEliminatoriaDTO>>> SelecionaEquipesEmValidacao(int id)
        {
            IEnumerable<ControleEliminatoriaDTO> equipes = await _repository.GetEquipesCredenciadasEmValidacao(id);

            if (equipes == null)
                return NotFound("Nenhuma equipe foi encontrada para essa etapa eliminatória");

            return Ok(equipes);
        }

        [HttpPut("{id}/equipes")]
        public async Task<ActionResult<ControleEliminatoria>> AlteraStatusValidacao(int id, AtualizarControleEliminatoriaDTO request)
        {
            if (request == null)
                return BadRequest("Dados inválidos");

            ControleEliminatoria equipeExistente = await _repository.GetControleEliminatoriaByIdAsync(id);

            if (equipeExistente == null)
                return NotFound("Equipe não encontrada");

            if (request.StatusValidacao != null)
                equipeExistente.StatusValidacao = request.StatusValidacao;

            if (request.Tempo != null)
                equipeExistente.Tempo = request.Tempo;

            if(request.StatusValidacao == "Aprovado")
            {
                equipeExistente.Pontuacao = request.Pontuacao;
            };

            if (request.StatusValidacao == "Declinado")
            {
                equipeExistente.Pontuacao = request.Pontuacao;
            };

            await _repository.UpdateAsync(equipeExistente);

            return Ok(equipeExistente.StatusValidacao);
        }

        [HttpPut("{idTorneio}/iniciarRodada")]
        public async Task<ActionResult<IEnumerable<ControleEliminatoria>>> IniciarRodada(int idTorneio)
        {
            if (idTorneio == 0)
                return BadRequest("Dados inválidos");

            IEnumerable<ControleEliminatoria> controlesEliminatoriasAtualizados
                = await _repository.IniciarNovaRodada(idTorneio);

            if (controlesEliminatoriasAtualizados == null)
                return NotFound("Nenhuma equipe foi encontrada");

            return Ok(controlesEliminatoriasAtualizados);
        }

        [HttpPut("{idTorneio}/finalizarRodadaAtual")]
        public async Task<ActionResult<IEnumerable<ControleEliminatoria>>> FinalizarRodadaAtual(int idTorneio)
        {
            if (idTorneio == 0)
                return BadRequest("Dados inválidos");

            IEnumerable<ControleEliminatoria> controlesEliminatoriasAtualizados
                = await _repository.FinalizarRodadaAtual (idTorneio);

            if (controlesEliminatoriasAtualizados == null)
                return NotFound("Nenhuma equipe foi encontrada");

            return Ok(controlesEliminatoriasAtualizados);
        }

        [HttpPut("{idTorneio}/finalizarEtapaEliminatoria")]
        public async Task<ActionResult<IEnumerable<ControleEliminatoria>>> FinalizarEtapaEliminatoria(int idTorneio)
        {
            if (idTorneio == 0)
                return BadRequest("Dados inválidos");

            IEnumerable<ControleEliminatoria> controlesEliminatoriasAtualizados
                = await _repository.FinalizarEtapaEliminatoria(idTorneio);

            if (controlesEliminatoriasAtualizados == null)
                return NotFound("Nenhuma equipe foi encontrada");

            return Ok(controlesEliminatoriasAtualizados);
        }
    }
}
