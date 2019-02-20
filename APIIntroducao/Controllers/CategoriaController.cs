using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIIntroducao.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIIntroducao.Controllers
{
    [Produces("application/json")]
    [Route("api/Categoria")]
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext context;
        
        public CategoriaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public IEnumerable<Categoria> Get()
        {
            return context.Categorias.ToList();
        }

        [HttpGet("{id}", Name = "categoriaCriada")]
        public IActionResult GetById(int id)
        {
            var cat = context.Categorias.Include(x => x.Produtos).FirstOrDefault(x => x.Id == id);
            if (cat == null)
            {
                return NotFound();
            }

            return Ok(cat);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Categoria cat)
        {
            if (ModelState.IsValid)
            {
                context.Categorias.Add(cat);
                context.SaveChanges();
                return new CreatedAtRouteResult("categoriaCriada", new { id = cat.Id }, cat);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Categoria cat, int id)
         {
            if (cat.Id != id)
            {
                return BadRequest(ModelState);
            }

            context.Entry(cat).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cat = context.Categorias.FirstOrDefault(x => x.Id == id);
            if(cat.Id != id)
            {
                return BadRequest(ModelState);
            }

            context.Categorias.Remove(cat);
            context.SaveChanges();
            return Ok();
        }
    }
}