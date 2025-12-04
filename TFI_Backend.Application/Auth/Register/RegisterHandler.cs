using BCrypt.Net;
using TFI_Backend.Core.Interfaces;
using TFI_Backend.Core.Model;

namespace TFI_Backend.Application.Auth.Register
{
    public class RegisterHandler
    {

        private readonly IUsuarioRepository _usuarioRepository;

        public RegisterHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<RegisterResponse>  Handle(RegisterCommand command)
        {
            var existingUser = _usuarioRepository.GetByEmail(command.Email);
            if (existingUser != null)
                throw new InvalidOperationException("El correo ya está registrado");

            var newUser = new User
            {
                Nombre = command.Nombre,
                Email = command.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password),
                Rol = command.Rol
            };

            _usuarioRepository.Add(newUser);
            _usuarioRepository.SaveChanges();

            return new RegisterResponse
            {
                Message = "Usuario registrado correctamente",
                Email = command.Email
            };
        }
    
    }
}
