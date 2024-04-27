using Codefast.Models;
using Codefast.Models.DTOs.Torneio;

namespace Codefast.Repository.Interfaces;

public interface ITorneioRepository : IBaseRepository
{
    Task<IEnumerable<TorneioDTO>> GetAllTorneiosAsync();
    Task<Torneio> GetTorneioByIdAsync(int id);
}



