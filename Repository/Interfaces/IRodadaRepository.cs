using Codefast.Models;

namespace Codefast.Repository.Interfaces
{
    public interface IRodadaRepository : IBaseRepository
    {
        Task<IEnumerable<Rodada>> SelecionaRodadaEmAndamento(int idTorneio);

    }
}
