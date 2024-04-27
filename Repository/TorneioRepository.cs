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
                    Nome = eq.Nome
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<Torneio> GetTorneioByIdAsync(int id)
    {
        return await _context.Torneios
            .Where(x => x.Id == id).Include(e => e.Equipes)
            .FirstOrDefaultAsync();
    }
}