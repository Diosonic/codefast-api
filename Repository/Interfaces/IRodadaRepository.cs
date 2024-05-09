using Codefast.Models;

namespace Codefast.Repository.Interfaces
{
    public interface IRodadaRepository : IBaseRepository
    {
        public void CriaRodadasEtapaMataMata(int idTorneio);
        Task<IEnumerable<Rodada>> SelecionaRodadaEmAndamento(int idTorneio);

    }
}
