﻿using Codefast.Context;
using Codefast.Models;
using Codefast.Models.DTOs.ControleEliminatoria;
using Codefast.Models.DTOs.Equipe;
using Codefast.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using ControleEliminatoria = Codefast.Models.ControleEliminatoria;

namespace Codefast.Repository
{
    public class ControleEliminatoriaRepository : BaseRepository, IControleEliminatoriaRepository
    {
        private readonly CodefastContext _context;

        public ControleEliminatoriaRepository(CodefastContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ControleEliminatoriaDTO>> GetEquipesCredenciadas(int id)
        {
            return await _context.ControleEliminatorias
                    .Where(eq => eq.Equipe.TorneioId == id && eq.Equipe.IsCredenciado)
                    .Select(eq => new ControleEliminatoriaDTO
                    {
                        Id = eq.Id,
                        Tempo = eq.Tempo,
                        Pontuacao = eq.Pontuacao,
                        StatusValidacao = eq.StatusValidacao,
                        Equipe = new EquipeDTO
                        {
                            Id = eq.EquipeId,
                            Nome = eq.Equipe.Nome,
                            IsDesclassificado = eq.Equipe.IsDesclassificado,

                        }
                    })
                    .OrderByDescending(eq => eq.Pontuacao)
                    .ToListAsync();
        }

        public async Task<IEnumerable<ControleEliminatoriaDTO>> GetEquipesCredenciadasEmValidacao(int id)
        {
            return await _context.ControleEliminatorias
                    .Where(eq => eq.Equipe.TorneioId == id && eq.Equipe.IsCredenciado)
                    .Select(eq => new ControleEliminatoriaDTO
                    {
                        Id = eq.Id,
                        Tempo = eq.Tempo,
                        Pontuacao = eq.Pontuacao,
                        StatusValidacao = eq.StatusValidacao,
                        Equipe = new EquipeDTO
                        {
                            Id = eq.EquipeId,
                            Nome = eq.Equipe.Nome,
                        }
                    }).Where(eq => eq.StatusValidacao == "Validando")
                    .ToListAsync();
        }

        public async Task<ControleEliminatoria> GetControleEliminatoriaByIdAsync(int id)
        {
            return await _context.ControleEliminatorias
                .Include(c => c.Equipe)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<ControleEliminatoriaDTO> GetControleEliminatoriaByIdAsyncList(int id)
        {
            return await _context.ControleEliminatorias
                .Include(c => c.Equipe)
                .Where(e => e.Id == id)
                   .Select(eq => new ControleEliminatoriaDTO
                   {
                       Id = eq.Id,
                       Tempo = eq.Tempo,
                       Pontuacao = eq.Pontuacao,
                       StatusValidacao = eq.StatusValidacao,
                       Equipe = new EquipeDTO
                       {
                           Id = eq.EquipeId,
                           Nome = eq.Equipe.Nome,
                       }
                   })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ControleEliminatoria>> IniciarNovaRodada(int idTorneio)
        {
            var controleEliminatorias = await _context.ControleEliminatorias
                 .Include(e => e.Equipe)
                 .Where(e => e.Equipe.TorneioId == idTorneio)
                 .ToListAsync();

            foreach (var controleEliminatoria in controleEliminatorias)
            {
                controleEliminatoria.StatusValidacao = "Em progresso";
                controleEliminatoria.Tempo = new TimeSpan(0, 0, 0);

                _context.Entry(controleEliminatoria.Equipe).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return controleEliminatorias;
        }

        public async Task<IEnumerable<ControleEliminatoria>> FinalizarRodadaAtual(int idTorneio)
        {
            var controleEliminatorias = await _context.ControleEliminatorias
                      .Include(e => e.Equipe)
                      .Where(e => e.Equipe.TorneioId == idTorneio)
                      .ToListAsync();

            foreach (var controleEliminatoria in controleEliminatorias)
            {
                controleEliminatoria.StatusValidacao = "Em espera";

                _context.Entry(controleEliminatoria.Equipe).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return controleEliminatorias;
        }

        public async Task<IEnumerable<ControleEliminatoria>> FinalizarEtapaEliminatoria(int idTorneio)
        {
            var controleEliminatorias = await _context.ControleEliminatorias
                      .Include(e => e.Equipe)
                      .Where(e => e.Equipe.TorneioId == idTorneio)
                      .ToListAsync();

            var classificados = await _context.ControleEliminatorias
                      .Include(e => e.Equipe)
                      .Where(e => e.Equipe.TorneioId == idTorneio)
                      .OrderByDescending(e => e.Pontuacao)
                      .Take(16)
                      .ToListAsync();

            if (classificados.Count != 16)
            {
                throw new Exception("Número de equipes classificadas diferente de 16.");
            }

            foreach (var controleEliminatoria in controleEliminatorias)
            {
                bool estaClassificado = classificados.Any(c => c.EquipeId == controleEliminatoria.EquipeId);

                if (!estaClassificado)
                {
                    controleEliminatoria.Equipe.IsDesclassificado = true;

                    _context.Entry(controleEliminatoria.Equipe).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }

            var ordenados = new List<ControleEliminatoria>();

            for (int i = 0; i < 8; i++)
            {
                ordenados.Add(classificados[i]);
                ordenados.Add(classificados[15 - i]);
            }


            foreach (var classificado in ordenados)
            {
                ControleMataMata controleMata = new ControleMataMata
                {
                    StatusValidacao = "Em progresso",
                    EquipeId = classificado.EquipeId
                };

                _context.Add(controleMata);
                await _context.SaveChangesAsync();
            }


            for (int i = 1; i < 5; i++)
            {
                string tituloRodada;

                if (i == 1)
                {
                    tituloRodada = "Oitava de final";
                }
                else if (i == 2)
                {
                    tituloRodada = "Quarta de final";
                }
                else if (i == 3)
                {
                    tituloRodada = "Semifinal";
                }
                else if (i == 4)
                {
                    tituloRodada = "Final";
                }
                else
                {
                    tituloRodada = " ";
                }

                Rodada rodada = new Rodada
                {
                    Titulo = tituloRodada,
                    TorneioId = idTorneio,
                    SementeRodadas = new List<SementeRodada>(),
                };

                if (i == 1)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        rodada.SementeRodadas.Add(new SementeRodada
                        {
                            Equipes = []
                        });

                    }
                }

                if (i == 2)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        rodada.SementeRodadas.Add(new SementeRodada
                        {
                            Equipes = []
                        });

                    }
                }

                if (i == 3)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        rodada.SementeRodadas.Add(new SementeRodada
                        {
                            Equipes = []
                        });
                    }
                }

                if (i == 4)
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
                await _context.SaveChangesAsync();
            }


            return controleEliminatorias;
        }
    }
}