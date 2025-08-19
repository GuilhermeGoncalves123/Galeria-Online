using GaleriaOnline.WebApi.Models;
namespace GaleriaOnline.WebApi.Interfaces

{
    public interface IImagensRepository
    {
        Task<IEnumerable<Imagem>> GetAllAsync();
        Task<Imagem?> GetByIdAsync(int id);
        Task<Imagem> CreateAsync(Imagem imagem);
        Task<bool> UpdateAsync(Imagem imagem);
        Task<bool> DeleteAsync(int id);
    }
}
