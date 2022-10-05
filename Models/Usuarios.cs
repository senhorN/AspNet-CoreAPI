using System;
using System.Collections.Generic;

namespace InventarioNet.Models
{
    public partial class Usuarios
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public string Email { get; set; }
    }
}
