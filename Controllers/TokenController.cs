using InventarioNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;



namespace InventarioNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TokenController : Controller
    {
        public IConfiguration _configuration;
        private readonly AppDbContext _context;
        public TokenController(IConfiguration config, AppDbContext context)
        {
            _configuration = config;
            _context = context;
        }
        
       



        #region Autenticação do usuario      
        [HttpPost]
       
        public async Task<ActionResult> AutenticaUsuario(Usuarios _usuario)
        {
            if (_usuario != null && _usuario.Email != null && _usuario.Senha != null)
            {
                
                Usuarios usuario = await GetUsuario(_usuario.Email, _usuario.Senha);
                if (usuario != null)
                {
                    //cria claims <infomações do usuario> baseado nas informações do usuário
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", usuario.UsuarioId.ToString()),
                    new Claim("Nome", usuario.Nome),
                    new Claim("Login", usuario.Login),
                    new Claim("Email", usuario.Email),
                    new Claim("Senha", usuario.Senha),
                    new Claim("Ativo", usuario.Ativo.ToString())
                };
                    //endereçamento da chave jwt
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                                 _configuration["Jwt:Audience"], claims,
                                 expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);


              

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Credenciais inválidas");

                   
                }
            }
            else
            {
                return BadRequest();
            }
            
        }
        #endregion
        private async Task<Usuarios> GetUsuario(string email, string password)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Senha == password);
        }

    }
}
