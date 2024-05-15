using Codefast.Context;
using Codefast.Models;
using Codefast.Models.DTOs.ControleMataMata;
using Codefast.Models.DTOs.Equipe;
using Codefast.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Codefast.Repository
{
    public class SementeRodadaRepository : BaseRepository, ISementeRodadaRepository
    {
        private readonly CodefastContext _context;

        public SementeRodadaRepository(CodefastContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IEnumerable<SementeRodada>> GetAllSementeRodadaAsync()
        {
            return await _context.SementeRodadas.ToListAsync();
        }

        public async Task<SementeRodada> PostSementeRodadaByIdAsync(int id, AdicionarChavesDTO request)
        {
            var equipe = await _context.Equipes
                .Include(e => e.SementeRodadas)
                .FirstOrDefaultAsync(e => e.Id == request.EquipeId);

            if (equipe == null)
                return null;

            var sementeRodada = await _context.SementeRodadas
                .FindAsync(request.SementeRodadaId);

            if (sementeRodada == null)
                return null;

            equipe.SementeRodadas.Add(sementeRodada);
            await _context.SaveChangesAsync();

            return sementeRodada;
        }
        

    }
}
