using Codefast.Context;
using Codefast.Models;
using Codefast.Models.DTOs.Equipe;
using Codefast.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Codefast.Repository;

public class EquipeRepository : BaseRepository, IEquipeRepository
{
    private readonly CodefastContext _context;

    public EquipeRepository(CodefastContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EquipeDTO>> GetAllEquipeAsync()
    {
        return await _context.Equipes
            .Select(e => new EquipeDTO
            {
                Id = e.Id,
                TituloTorneio = e.Torneio.Titulo,
                Nome = e.Nome,
                NomeParticipantes = e.NomeParticipantes,
                IsCredenciado = e.IsCredenciado
            })
            .ToListAsync();
    }

    public async Task<Equipe> GetEquipeByIdAsync(int id)
    {
        return await _context.Equipes
            .Where(e => e.Id == id)
            .Include(e => e.ControleEliminatoria)
            .FirstOrDefaultAsync();
    }
}

