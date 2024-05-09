using Codefast.Context;
using Codefast.Models;
using Codefast.Models.DTOs.ControleMataMata;
using Codefast.Models.DTOs.Equipe;
using Codefast.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Codefast.Repository;

public class ControleMataMataRepository : BaseRepository, IControleMataMataRepository
{
    private readonly CodefastContext _context;

    public ControleMataMataRepository(CodefastContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EquipeDTO>> GetEquipesParaFaseMataMata(int idTorneio)
    {
        return await _context.Equipes
            .Where(e => e.TorneioId == idTorneio && e.IsDesclassificado == false)
            .Select(eq => new EquipeDTO
            {
                Id = eq.Id,
                Nome = eq.Nome,
                NomeParticipantes = eq.NomeParticipantes,
                IsCredenciado = eq.IsCredenciado
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<ControleMataMataDTO>> GetControleMataMataAsync(int idTorneio)
    {
        return await _context.ControleMataMatas
            .Where(c => c.Equipe.TorneioId == idTorneio)
            .Select(eq => new ControleMataMataDTO
            {
                Id = eq.Id,
                StatusValidacao = eq.StatusValidacao,

                Equipe = new ControleMataMataEquipeDTO
                {
                    Id = eq.EquipeId,
                    Nome = eq.Equipe.Nome,
                    IsDesclassificado = eq.Equipe.IsDesclassificado
                }
            })
            .ToListAsync();
    }
}
