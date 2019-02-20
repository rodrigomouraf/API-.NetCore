using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIIntroducao.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIIntroducao.Controllers
{
    [Produces("application/json")]
    [Route("api/Categoria/{CategoriaId}/Produto")]
    public class ProdutoController : Controller
    {
        private readonly ApplicationDbContext context;

        public ProdutoController(ApplicationDbContext context )
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Produto> Get(int categoriaId)
        {
            return context.Produtos.Where(x => x.CategoriaId == categoriaId).ToList();
        }

        public IActionResult GetById(int id)
        {
            var cat = context.Produtos.FirstOrDefault(x => x.Id == id);
            if(cat == null)
            {
                return NotFound();
            }

            return Ok(cat);
        }
    }
}