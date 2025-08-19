using GaleriaOnline.WebApi.DTO;
using GaleriaOnline.WebApi.Interfaces;
using GaleriaOnline.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace GaleriaOnline.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagemController : ControllerBase
    {
        private readonly IImagensRepository _repository;
        private readonly IWebHostEnvironment _env;

        public ImagemController(IImagensRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImagemPorID(int id)
        {
            var imagem = await _repository.GetByIdAsync(id);
            if (imagem == null)
            {
                return NotFound();
            }

            return Ok(imagem);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodasAsimagens()
        {
            var imagens = await _repository.GetAllAsync();
            return Ok(imagens);
        }


        [HttpPost("upload")]

        public async Task<IActionResult> UploadImagem([FromForm] ImagemDTO dto)
        {
            if (dto.Arquivo == null || dto.Arquivo.Length == 0 || String.IsNullOrWhiteSpace(dto.nome))
            {
                return BadRequest("Nenhum arquivo enviado.");
            }

            var extensao = Path.GetExtension(dto.Arquivo.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            if (!Directory.Exists(caminhoPasta))
            {
                Directory.CreateDirectory(caminhoPasta);
            }

            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await dto.Arquivo.CopyToAsync(stream);
            }

            var imagem = new Imagem
            {
                Nome = dto.nome,
                Caminho = Path.Combine(pastaRelativa, nomeArquivo).Replace("\\", "/")
            };

            await _repository.CreateAsync(imagem);

            return CreatedAtAction(nameof(GetImagemPorID), new { id = imagem.Id }, imagem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarImagem(int id, PutImagem imagemAtualizada)
        {
            var imagem = await _repository.GetByIdAsync(id);
            if (imagem == null)
            {
                return NotFound("Imagem nçao encontrada");

            }
            if (imagemAtualizada.Arquivo == null && string.IsNullOrWhiteSpace(imagemAtualizada.Nome))
            {
                return BadRequest("Pelo menos um dos campos tem que ser preenchido.");
            }

            if (!string.IsNullOrWhiteSpace(imagemAtualizada.Nome))
            {
                imagem.Nome = imagemAtualizada.Nome;
            }

            var caminho = Path.Combine(Directory.GetCurrentDirectory(), imagem.Caminho.Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (imagemAtualizada.Arquivo != null && imagemAtualizada.Arquivo.Length > 0)
            {
                if (System.IO.File.Exists(caminho))
                {
                    System.IO.File.Delete(caminho);
                }
                var extensao = Path.GetExtension(imagemAtualizada.Arquivo.FileName);
                var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

                var pastaRelativa = "wwwroot/imagens";
                var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

                if (!Directory.Exists(caminhoPasta))
                {
                    Directory.CreateDirectory(caminhoPasta);
                }

                var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await imagemAtualizada.Arquivo.CopyToAsync(stream);
                }
                imagem.Caminho = Path.Combine(pastaRelativa, nomeArquivo).Replace("\\", "/");
            }

            var atualizada = await _repository.UpdateAsync(imagem);
            if (!atualizada)
            {
                return StatusCode(500, "erro ao atualizar a imagem");
            }
            return Ok(imagem);


        }

        public Task<IActionResult> DeletarImagem(int id)
        {
            return DeletarImagem(id, Imagem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeletarImagem(int id, Imagem imagem)
        {
            var caminhoFisico = Path.Combine(Directory.GetCurrentDirectory(),
imagem.Caminho.Replace("/",
     Path.DirectorySeparatorChar.ToString()

     ));
            if (System.IO.File.Exists(caminhoFisico))
            {
                try
                {
                    System.IO.File.Delete(caminhoFisico);

                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Erro Ao excluir o arquivo : {ex.Message}");
                }
            }
            var deletado = await _repository.DeleteAsync(id);
            if (!deletado)
            {
                return StatusCode(500, "Erro ao excluir imagem do banco");

            }
            return NoContent();
        }


    }
}
