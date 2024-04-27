using Codefast.Models.DTOs.Equipe;
using Codefast.Models;
using Codefast.Models.DTOs.Torneio;
using Codefast.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Codefast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TorneioController : ControllerBase
    {
        private readonly ITorneioRepository _repository;

        public TorneioController(ITorneioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<TorneioDTO> torneios = await _repository.GetAllTorneiosAsync();

            return Ok(torneios);
        }


        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            Torneio torneio = await _repository.GetTorneioByIdAsync(id);

            if (torneio == null)
                return NotFound("Torneio não encontrado");

            return Ok(torneio);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AdicionarTorneioDTO request)
        {
            if (request == null)
                return BadRequest("Dados inválidos");

            Torneio torneio = new Torneio
            {
                Titulo = request.Titulo
            };

            await _repository.AddAsync(torneio);

            return Ok(torneio);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Put(int id, AtualizarTorneioDTO request)
        {
            if (request == null)
                return BadRequest("Dados inválidos");

            Torneio torneioExistente = await _repository.GetTorneioByIdAsync(id);

            if (torneioExistente == null)
                return NotFound("Torneio não encontrado");

            torneioExistente.Titulo = request.Titulo;

            await _repository.UpdateAsync(torneioExistente);

            return Ok(torneioExistente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Torneio torneioExistente = await _repository.GetTorneioByIdAsync(id);

            if (torneioExistente == null)
                return NotFound("Torneio não encontrado");

            await _repository.DeleteAsync(torneioExistente);

            return NoContent();
        }
    }
}
