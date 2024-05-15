using Codefast.Context;
using Codefast.Models;
using Codefast.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Codefast.Repository
{
    public class RodadaRepository : BaseRepository, IRodadaRepository
    {
        private readonly CodefastContext _context;

        public RodadaRepository(CodefastContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rodada>> SelecionaRodadaEmAndamento(int idTorneio)
        {
            return await _context.Rodadas
                .Where(c => c.TorneioId == idTorneio)
                .Select(r => new Rodada
                {
                    Id = r.Id,
                    Titulo = r.Titulo,
                    SementeRodadas = r.SementeRodadas.Select(sr => new SementeRodada
                    {
                        Id = sr.Id,
                        RodadaId = sr.RodadaId,
                        Equipes = sr.Equipes.Select(eq => new Equipe
                        {
                            Id = eq.Id,
                            IsCredenciado = eq.IsCredenciado,
                            Nome = eq.Nome,
                            IsDesclassificado = eq.IsDesclassificado,
                            NomeParticipantes = eq.NomeParticipantes,
                            ControleMataMata = eq.ControleMataMata
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();
        }
    }
}
