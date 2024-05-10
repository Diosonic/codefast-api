using Codefast.Context;
using Codefast.Migrations;
using Codefast.Models;
using Codefast.Models.DTOs.ControleEliminatoria;
using Codefast.Models.DTOs.Equipe;
using Codefast.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
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
                        IsDesclassificado = eq.IsDesclassificado,
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
                       IsDesclassificado = eq.IsDesclassificado,
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

    }
}