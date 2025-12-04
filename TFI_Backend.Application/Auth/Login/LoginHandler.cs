using Microsoft.Extensions.Configuration;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TFI_Backend.Core.Interfaces;
using TFI_Backend.Core.Model;


namespace TFI_Backend.Application.Auth.Login
{
    public class LoginHandler
    {

       private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _config;

        public LoginHandler(IUsuarioRepository usuarioRepository, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _config = config;
        }

        public async Task<LoginResponse>  Handle(LoginCommand command)
        {
            var user = _usuarioRepository.GetByEmail(command.Email);
            string hash = BCrypt.Net.BCrypt.HashPassword(command.Password);
            if (user == null || !BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Credenciales inválidas");

            var token = GenerateJwtToken(user);

            return new LoginResponse
            {
                Token = token,
                Nombre = user.Nombre,
                Rol = user.Rol
            };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Rol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
