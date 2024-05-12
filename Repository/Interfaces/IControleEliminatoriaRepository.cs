using Codefast.Models;
using Codefast.Models.DTOs.ControleEliminatoria;

namespace Codefast.Repository.Interfaces
{
    public interface IControleEliminatoriaRepository : IBaseRepository
    {
        Task<IEnumerable<ControleEliminatoriaDTO>> GetEquipesCredenciadas(int id);
        Task<IEnumerable<ControleEliminatoriaDTO>> GetEquipesCredenciadasEmValidacao(int id);
        Task<ControleEliminatoria> GetControleEliminatoriaByIdAsync(int id);
        Task<ControleEliminatoriaDTO> GetControleEliminatoriaByIdAsyncList(int id);
        Task<IEnumerable<ControleEliminatoria>> IniciarNovaRodada(int idTorneio);
        Task<IEnumerable<ControleEliminatoria>> FinalizarRodadaAtual(int idTorneio);
        Task<IEnumerable<ControleEliminatoria>> FinalizarEtapaEliminatoria(int idTorneio);

    }
}
