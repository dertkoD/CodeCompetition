namespace BlazorApp1.Services
{
    public class Token
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Token(IHttpContextAccessor httpContextAccessor) { _httpContextAccessor = httpContextAccessor; }
        public string? GetToken()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["auth_token"];
            return token;
        }
    }
}
