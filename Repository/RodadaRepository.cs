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

        public void CriaRodadasEtapaMataMata(int idTorneio)
        {

            for (int i = 1; i < 4; i++)
            {
                Rodada rodada = new Rodada
                {
                    Titulo = i + "ª Rodada",
                    TorneioId = idTorneio,
                    SementeRodadas = new List<SementeRodada>(),
                };

                if (i == 1)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        rodada.SementeRodadas.Add(new SementeRodada
                        {
                            Equipes = []
                        });

                    }
                }

                if (i == 2)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        rodada.SementeRodadas.Add(new SementeRodada
                        {
                            Equipes = []
                        });

                    }
                }

                if (i == 3)
                {
                    for (int j = 0; j < 1; j++)
                    {
                        rodada.SementeRodadas.Add(new SementeRodada
                        {
                            Equipes = []
                        });
                    }
                }

                _context.Add(rodada);
                _context.SaveChanges();
            }
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
                            NomeParticipantes = eq.NomeParticipantes,
                            ControleMataMata = eq.ControleMataMata
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();
        }
    }
}
