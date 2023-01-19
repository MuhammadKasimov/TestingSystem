using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features.Authentication;
using System.Linq;

namespace TestingSystem.Service.Helpers
{
    public class HttpContextHelper
    {
        public static IHttpContextAccessor Accessor { get; set; }
        public static HttpContext HttpContext => Accessor?.HttpContext;
        public static string DeviceId => HttpContext?.Request.Headers["X-Device-Id"].FirstOrDefault();
        public static IHeaderDictionary ResponseHeaders => HttpContext?.Response?.Headers;
        public static int? UserId => GetUserId();
        public static string IpAddress => HttpContext?.Connection.RemoteIpAddress.ToString();
        public static string UserRole => HttpContext?.User.FindFirst("Role")?.Value;

        private static int? GetUserId()
        {
            string value = HttpContext?.User?.Claims.FirstOrDefault(p => p.Type == "Id")?.Value;

            bool canParse = int.TryParse(value, out int id);
            return canParse ? id : null;
        }
    }
}
