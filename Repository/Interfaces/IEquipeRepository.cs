using Codefast.Models;
using Codefast.Models.DTOs.Equipe;

namespace Codefast.Repository.Interfaces;

public interface IEquipeRepository : IBaseRepository
{
    Task<IEnumerable<EquipeDTO>> GetAllEquipeAsync();
    Task<Equipe> GetEquipeByIdAsync(int id);
}



