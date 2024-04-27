using Microsoft.AspNetCore.Mvc;
using Codefast.Repository.Interfaces;
using Codefast.Models.DTOs.Equipe;
using Codefast.Models;
using Azure.Core;

namespace Codefast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipeController : ControllerBase
    {
        private readonly IEquipeRepository _repository;

        public EquipeController(IEquipeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var equipes = await _repository.GetAllEquipeAsync();

            return Ok(equipes);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var equipe = await _repository.GetEquipeByIdAsync(id);

            if (equipe == null)
                return NotFound("Equipe não encontrada");
   
            return Ok(equipe);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AdicionarEquipeDTO request)
        {
            if (request == null)
                return BadRequest("Dados inválidos");

            var equipeDTO = new Equipe
            {
                Nome = request.Nome,
                NomeParticipantes = request.NomeParticipantes,
                TorneioId = request.TorneioId
            };

            await _repository.AddAsync(equipeDTO);  

            return Ok(equipeDTO);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Put(int id, AtualizarEquipeDTO request)
        {
            if (request == null)
                return BadRequest("Dados inválidos");

            var equipeExistente = await _repository.GetEquipeByIdAsync(id);

            if (equipeExistente == null)
                return NotFound("Equipe não encontrada");

            equipeExistente.Nome = request.Nome;
            equipeExistente.IsCredenciado = request.IsCredenciado;
            equipeExistente.IsDesclassificado = request.IsDesclassificado;
            equipeExistente.Pontuacao = request.Pontuacao;
            equipeExistente.StatusValidacao = request.StatusValidacao;
            equipeExistente.NomeParticipantes = request.NomeParticipantes;

            await _repository.UpdateAsync(equipeExistente);

            return Ok(equipeExistente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var equipeExistente = await _repository.GetEquipeByIdAsync(id);

            if (equipeExistente == null)
                return NotFound("Equipe não encontrada");

            await _repository.DeleteAsync(equipeExistente);

            return NoContent(); 
        }
    }
}




