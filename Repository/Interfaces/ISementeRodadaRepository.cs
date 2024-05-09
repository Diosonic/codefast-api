using Codefast.Models;
using Codefast.Models.DTOs.ControleMataMata;

namespace Codefast.Repository.Interfaces;

public interface ISementeRodadaRepository : IBaseRepository
{
    Task<SementeRodada> GetSementeRodadaByIdAsync(int id, AdicionarChavesDTO request);
}
