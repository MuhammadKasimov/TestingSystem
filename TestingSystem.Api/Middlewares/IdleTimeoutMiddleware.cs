using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using System;
using TestingSystem.Api.Helpers;

namespace TestingSystem.Api.Middlewares
{
    public class IdleTimeoutMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TimeSpan _timeout;

        public IdleTimeoutMiddleware(RequestDelegate next, TimeSpan timeout)
        {
            _next = next;
            _timeout = timeout;
        }

        public async Task Invoke(HttpContext context)
        {
            var userSession = context.Session.GetString("UserSession");
            if (userSession != null)
            {
                var session = JsonConvert.DeserializeObject<UserSession>(userSession);
                if ((DateTime.Now - session.LastActivity).TotalMinutes > _timeout.TotalMinutes)
                {
                    context.Session.Remove("UserSession");
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }
                session.LastActivity = DateTime.Now;
                context.Session.SetString("UserSession", JsonConvert.SerializeObject(session));
            }
            await _next.Invoke(context);
        }
    }
}
