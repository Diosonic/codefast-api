using Codefast.Context;
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
                        EquipeId = eq.EquipeId,
                    })
                    .ToListAsync();
        }
    }
}