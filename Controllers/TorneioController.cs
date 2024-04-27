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
    }
}
