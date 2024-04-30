using Microsoft.AspNetCore.Mvc;
using Codefast.Repository.Interfaces;
using Codefast.Models.DTOs.Equipe;
using Codefast.Models;

namespace Codefast.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EquipeController : ControllerBase
    {
        private readonly IEquipeRepository _repository;

        public EquipeController(IEquipeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipeDTO>>> GetAll()
        {
            IEnumerable<EquipeDTO> equipes = await _repository.GetAllEquipeAsync();

            return Ok(equipes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Equipe>> GetById(int id)
        {
            Equipe equipe = await _repository.GetEquipeByIdAsync(id);

            if (equipe == null)
                return NotFound("Equipe não encontrada");
   
            return Ok(equipe);
        }

        [HttpPost]
        public async Task<ActionResult<Equipe>> Post(AdicionarEquipeDTO request)
        {
            if (request == null)
                return BadRequest("Dados inválidos");

            Equipe equipe = new Equipe
            {
                Nome = request.Nome,
                NomeParticipantes = request.NomeParticipantes,
                TorneioId = request.TorneioId,
            };

            ControleEliminatoria controleEliminatoria = new ControleEliminatoria
            {
                StatusValidacao = "Pendente",
                IsDesclassificado = false,
                Pontuacao = 0,
                Equipe = equipe
            };

            equipe.ControleEliminatoria = controleEliminatoria;

            await _repository.AddAsync(equipe);  

            return Ok(equipe);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Equipe>> Put(int id, AtualizarEquipeDTO request)
        {
            if (request == null)
                return BadRequest("Dados inválidos");

            Equipe equipeExistente = await _repository.GetEquipeByIdAsync(id);

            if (equipeExistente == null)
                return NotFound("Equipe não encontrada");

            if (request.Nome != null)
                equipeExistente.Nome = request.Nome;

            if (request.IsCredenciado.HasValue)
                equipeExistente.IsCredenciado = request.IsCredenciado.Value;

            if (request.NomeParticipantes != null)
                equipeExistente.NomeParticipantes = request.NomeParticipantes;

            await _repository.UpdateAsync(equipeExistente);

            return Ok(equipeExistente);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Equipe equipeExistente = await _repository.GetEquipeByIdAsync(id);

            if (equipeExistente == null)
                return NotFound("Equipe não encontrada");

            await _repository.DeleteAsync(equipeExistente);

            return NoContent(); 
        }
    }
}




