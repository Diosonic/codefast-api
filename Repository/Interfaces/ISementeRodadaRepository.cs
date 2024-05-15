using Codefast.Models;
using Codefast.Models.DTOs.ControleMataMata;

namespace Codefast.Repository.Interfaces;

public interface ISementeRodadaRepository : IBaseRepository
{
    Task<IEnumerable<SementeRodada>> GetAllSementeRodadaAsync();
    Task<SementeRodada> PostSementeRodadaByIdAsync(int id, AdicionarChavesDTO request);
}
