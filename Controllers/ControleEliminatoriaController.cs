using Codefast.Models;
using Codefast.Models.DTOs.ControleEliminatoria;
using Codefast.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{id}/equipes")]
        public async Task<ActionResult<IEnumerable<ControleEliminatoriaDTO>>> SelecionaEquipesCredenciadas(int id)
        {
            IEnumerable<ControleEliminatoriaDTO> equipes = await _repository.GetEquipesCredenciadas(id);

            if (equipes == null)
                return NotFound("Nenhuma equipe foi encontrada para essa etapa eliminatória");


            return Ok(equipes);
        }

        [HttpPut("{id}/equipes")]
        public async Task<ActionResult<ControleEliminatoriaDTO>> AlteraStatusValidacao(int id, AtualizarControleEliminatoriaDTO request)
        {
            if (request == null)
                return BadRequest("Dados inválidos");

            ControleEliminatoria equipeExistente = await _repository.GetControleEliminatoriaByIdAsync(id);

            if (equipeExistente == null)
                return NotFound("Equipe não encontrada");

            if (request.StatusValidacao != null)
                 equipeExistente.StatusValidacao = request.StatusValidacao;

            await _repository.UpdateAsync(equipeExistente);

            return Ok(equipeExistente.StatusValidacao);
        }
    }
}
