using Microsoft.AspNetCore.Http;
using SmHm.Core.Abstractions;

namespace SmHm.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _context;

        public CurrentUserService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public Guid UserId
        {
            get
            {
                var userIdClaim = _context.HttpContext?.User.FindFirst("userId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                {
                    throw new UnauthorizedAccessException("User ID not found in token.");
                }
                return userId;
            }
        }

        public bool IsAuthenticated => _context.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    }
}
