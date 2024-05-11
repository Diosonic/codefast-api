using Codefast.Models.DTOs.Equipe;
using Codefast.Models;
using Codefast.Models.DTOs.Torneio;
using Codefast.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Codefast.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TorneioController : ControllerBase
    {
        private readonly ITorneioRepository _repository;

        public TorneioController(ITorneioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("teste")]
        public async Task<ActionResult<IEnumerable<TorneioDTO>>> GetTorneioTEste()
        {

            return Ok("Rota criada");
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TorneioDTO>>> GetAll()
        {
            IEnumerable<TorneioDTO> torneios = await _repository.GetAllTorneiosAsync();

            return Ok(torneios);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Torneio>> GetById(int id)
        {
            Torneio torneio = await _repository.GetTorneioByIdAsync(id);

            if (torneio == null)
                return NotFound("Torneio não encontrado");

            return Ok(torneio);
        }

        [HttpGet("{id}/equipes")]
        public async Task<ActionResult<IEnumerable<EquipeDTO>>> GetAllEquipesTorneio(int id)
        {
            IEnumerable<EquipeDTO> equipes = await _repository.GetAllEquipesTorneioAsync(id);

            if(equipes == null)
            {
                return NotFound("Nenhuma equipe foi encontrada para esse torneio");
            }

            return Ok(equipes);
        }

        [HttpPost]
        public async Task<ActionResult<Torneio>> Post(AdicionarTorneioDTO request)
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

        [HttpPut("{id}")]
        public async Task<ActionResult<Torneio>> Put(int id, AtualizarTorneioDTO request)
        {
            if (request == null)
                return BadRequest("Dados inválidos");

            Torneio torneioExistente = await _repository.GetTorneioByIdAsync(id);

            if (torneioExistente == null)
                return NotFound("Torneio não encontrado");

            if(request.Titulo != null)
            {
                torneioExistente.Titulo = request.Titulo;
            }

            await _repository.UpdateAsync(torneioExistente);

            return Ok(torneioExistente);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Torneio torneioExistente = await _repository.GetTorneioByIdAsync(id);

            if (torneioExistente == null)
                return NotFound("Torneio não encontrado");

            await _repository.DeleteAsync(torneioExistente);

            return NoContent();
        }
    }
}
