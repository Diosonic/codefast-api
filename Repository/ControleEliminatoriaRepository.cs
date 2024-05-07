using Codefast.Context;
using Codefast.Models;
using Codefast.Models.DTOs.ControleEliminatoria;
using Codefast.Models.DTOs.Equipe;
using Codefast.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                        IsDesclassificado = eq.IsDesclassificado,
                        Equipe = new EquipeDTO
                        {
                            Id = eq.EquipeId,
                            Nome = eq.Equipe.Nome,
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

        public async Task<ControleEliminatoriaDTO> GetControleEliminatoriaByIdAsync(int id)
        {
            return await _context.ControleEliminatorias
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



    }
}