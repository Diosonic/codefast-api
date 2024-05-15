using Codefast.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Codefast.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SementeRodadaController : ControllerBase
    {
        private readonly ISementeRodadaRepository _repository;

        public SementeRodadaController(ISementeRodadaRepository repository)
        {
            _repository = repository;
        }
    }
}
