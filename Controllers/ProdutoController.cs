using AprendendoAPI.Models;
using AprendendoAPI.ViewModel;
using AprendendoAPI.Infraestrutura;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace AprendendoAPI.Controllers
{
    [ApiController]
    [Route("api/produto")]
    public class ProdutoController : ControllerBase
    {
        

        private readonly IProdutoRepository _produtoRepository;
        

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
            
        }

        [HttpGet]
        public IActionResult Get()
        {
            var produto = _produtoRepository.Get();
            return Ok(produto);
        }


        [HttpPost]
        public IActionResult Add([FromForm] ProdutoViewModel produtoView)
        {
            var filePath = Path.Combine("Storage", produtoView.foto.FileName);

            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            produtoView.foto.CopyTo(fileStream);

            var imagemUrl = $"https://localhost:7027/imagens/{produtoView.foto.FileName}";

            var produto = new Produto(produtoView.Nome,produtoView.Preco, produtoView.Descricao, imagemUrl);

            produto.Categoria = "comida";

            if (produtoView.Descricao.Length > 150)
            {
                return BadRequest("A descrição deve ter no maximo 100 caracteres");
            }

            _produtoRepository.Add(produto);

            var response = new
            {
                id = produto.Id,
                nome = produto.Nome,
                descricao = produto.Descricao,
                preco = produto.Preco.ToString("N2", new CultureInfo("pt-BR")),
                imagemUrl = produto.ImagemUrl
            };

            return Ok(produto);
        }

        [HttpGet("{id}")]
        public IActionResult GetPorId(int id)
        {
            var produto = _produtoRepository.BuscarPorId(id);

            if(produto == null)
            {
                return NotFound("Produto não encontrado.");
            }
            return Ok(produto);
        }

        [HttpPut]
        public IActionResult Update(int id, ProdutoViewModel produtoUpdateView)
        {
            var produto = _produtoRepository.BuscarPorId(id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }

            produto.Nome = produtoUpdateView.Nome;
            produto.Preco = produtoUpdateView.Preco;
            produto.Descricao = produtoUpdateView.Descricao;

            _produtoRepository.Update(produto);
            return Ok(produto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var produto = _produtoRepository.BuscarPorId(id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }
            _produtoRepository.Delete(produto);
            return Ok();
        }
    }
}
