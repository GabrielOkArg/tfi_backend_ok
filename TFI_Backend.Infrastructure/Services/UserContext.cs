using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TFI_Backend.Core.Interfaces;

namespace TFI_Backend.Infrastructure.Services
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string? GetEmail()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
             ?? _httpContextAccessor.HttpContext?.User?.FindFirst("email")?.Value;
        }

        public string? GetRole()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
        }

        public int? GetUserId()
        {
            var id = _httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value;
            return id != null ? int.Parse(id) : null;
        }
    }
}
