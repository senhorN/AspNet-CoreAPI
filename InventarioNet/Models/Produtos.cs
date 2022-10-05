using System;
using System.Collections.Generic;

namespace InventarioNet.Models
{
    public partial class Produtos
    {
        public int ProdutoId { get; set; } 
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public string Imagem { get; set; }
    }
}
