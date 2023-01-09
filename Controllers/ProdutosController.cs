using InventarioNet.Models;
using InventarioNet.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;


#region atributos 
// AllowAnonymous > permite acesso anonimo ou seja sem autenticação
//Authorize - requer que o usuário esteja apenas autenticado sem considerar o perfil
//Authorize(roles="Perfil1, Perfil2")- Exige que ususario esteja autenticado e que faça parte de um dos perfil definidos
#endregion 



namespace InventarioNet.Controllers
{
    [Authorize]//usuario seja autenticado sem considerara o perfil 

    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProdutosController : Controller
    {
        private readonly IProdutoRepository repository;
        public ProdutosController(IProdutoRepository _context)
        {
            repository = _context;
        }
        #region Get  
        [AllowAnonymous] //acesso anonimo ou seja autenticação
        [HttpGet("Get")] //requerimento "Get"
        public ActionResult<string> Get() //quando o sistema é acessado 
        {
            return "ProdutosController ::  Acessado em  : " + DateTime.Now.ToLongDateString();
        }
        #endregion
        #region Get produtos 
        [AllowAnonymous]
        [HttpGet ("GetProdutos")] ///método produtos 
        public async Task<ActionResult<IEnumerable<Produtos>>> GetProdutos()
        {
            var produtos = await repository.GetAll();
            if (produtos == null)
            {
                return BadRequest();
            }
            return Ok(produtos.ToList());
        }
        #endregion
        #region Get produto
        [AllowAnonymous]
        [HttpGet("GetProduto")] ///método todos 
        public async Task<ActionResult<Produtos>> GetProduto(int id)
        {
            var produto = await repository.GetById(id);
            if (produto == null)
            {
                return NotFound("Produto não encontrado pelo id informado");
            }
            return Ok(produto);
        }
        #endregion

        #region Post produto 
        // POST api/<controller>  
        [AllowAnonymous]
        [HttpPost("PostProduto")]
        public async Task<IActionResult> PostProduto([FromBody] Produtos produto)
        {
            if (produto == null)
            {
                return BadRequest("Produto é null");
            }
            await repository.Insert(produto);
            return CreatedAtAction(nameof(GetProduto), new { Id = produto.ProdutoId }, produto);
        }
        #endregion
        #region Put Produto

        [AllowAnonymous]
        [HttpPut("PutProduto/{id}")]
        public async Task<IActionResult> PutProduto(int id, Produtos produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest($"O código do produto {id} não confere");
            }
            try
            {
                await repository.Update(id, produto);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok("Atualização do produto realizada com sucesso");
        }
        #endregion
        #region Delete Produto 
        [AllowAnonymous]
        [HttpDelete("DeleteProduto/{id}")]
        public async Task<ActionResult<Produtos>> DeleteProduto(int id)
        {
            var produto = await repository.GetById(id);
            if (produto == null)
            {
                return NotFound($"Produto de {id} foi não encontrado");
            }
            await repository.Delete(id);
            return Ok(produto);
        }
        #endregion 

    }
}
