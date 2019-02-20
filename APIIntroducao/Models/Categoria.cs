using APIIntroducao.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIIntroducao
{
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new List<Produto>();
        }

        public int Id { get; set; }
        [StringLength(30)]
        public string Nome { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}
