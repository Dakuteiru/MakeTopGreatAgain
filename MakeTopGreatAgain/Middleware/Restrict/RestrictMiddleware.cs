using System.Net;

namespace MakeTopGreatAgain.Middleware.Restrict;

public class RestrictMiddleware(
    RequestDelegate next,
    ILogger<RestrictMiddleware> logger
)
{
    public async Task Invoke(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        var restrict = endpoint?.Metadata.GetMetadata<RestrictAttribute>();
        //logger.LogInformation("Restrict request in");
        
       if (restrict == null)
        {
            await next(context);
        }
        else
        {
            var allowed = restrict.Allowed.Contains(context.Connection.RemoteIpAddress);

            if (!allowed)
            {
                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                logger.LogWarning("Restrict access for{Ip}", context.Connection.RemoteIpAddress);
                throw new Exception("Restrict access forbidden");
            }
            else
                await next(context);
        }
        //logger.LogInformation("Restrict response out");
    }

}