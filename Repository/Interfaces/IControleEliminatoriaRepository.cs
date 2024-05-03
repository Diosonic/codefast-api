using Codefast.Models;
using Codefast.Models.DTOs.ControleEliminatoria;

namespace Codefast.Repository.Interfaces
{
    public interface IControleEliminatoriaRepository : IBaseRepository
    {
        Task<IEnumerable<ControleEliminatoriaDTO>> GetEquipesCredenciadas(int id);
        Task<ControleEliminatoria> GetControleEliminatoriaByIdAsync(int id);

    }
}
