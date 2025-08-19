using GaleriaOnline.WebApi.DbContextImagem;
using GaleriaOnline.WebApi.Interfaces;
using GaleriaOnline.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GaleriaOnline.WebApi.Repositories
{
    public class ImagemRepository : IImagensRepository
    {
        private readonly GaleriaOnlineDbContext _context;

        public ImagemRepository(GaleriaOnlineDbContext context)
        {
            _context = context;
        }

        public async Task<Imagem> CreateAsync(Imagem imagem)
        {
           _context.Imagens.Add(imagem);
            await _context.SaveChangesAsync();
            return imagem;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var imagem = await _context.Imagens.FindAsync(id);
            if (imagem != null)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Imagem>> GetAllAsync()
        {
            return await _context.Imagens.ToListAsync();
        }

        public async Task<Imagem?> GetByIdAsync(int id)
        {
            return await _context.Imagens.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Imagem imagem)
        {
            _context.Imagens.Update(imagem);
            return await _context.SaveChangesAsync() > 0;

        }
    }
}
