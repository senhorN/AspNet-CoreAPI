using InventarioNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioNet.Repositories
{

    public class ProdutoRepository : GenericRepository<Produtos>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext repositoryContext)
             : base(repositoryContext)
        {
        }
    }
}
