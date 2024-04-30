using Codefast.Context;
using Codefast.Models;
using Codefast.Models.DTOs.Equipe;
using Codefast.Models.DTOs.Torneio;
using Codefast.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Codefast.Repository;

public class TorneioRepository : BaseRepository, ITorneioRepository
{
    private readonly CodefastContext _context;

    public TorneioRepository(CodefastContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TorneioDTO>> GetAllTorneiosAsync()
    {
        return await _context.Torneios
            .Select(t => new TorneioDTO
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Equipes = t.Equipes.Select(eq => new EquipeDTO
                {
                    Id = eq.Id,
                    Nome = eq.Nome,
                    IsCredenciado = eq.IsCredenciado,
                    NomeParticipantes = eq.NomeParticipantes
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<EquipeDTO>> GetAllEquipesTorneioAsync(int id)
    {
        return await _context.Equipes
                .Where(eq => eq.TorneioId == id)
                .Select(eq => new EquipeDTO
                {
                    Id = eq.Id,
                    IsCredenciado = eq.IsCredenciado,
                    NomeParticipantes = eq.NomeParticipantes
                })
                .ToListAsync();
    }

    public async Task<Torneio> GetTorneioByIdAsync(int id)
    {
        return await _context.Torneios
            .Where(x => x.Id == id)
            .Include(e => e.Equipes)
            .FirstOrDefaultAsync();
    }
}