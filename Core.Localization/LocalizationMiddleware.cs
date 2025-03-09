using Microsoft.AspNetCore.Http;
using System.Collections.Immutable;

namespace Core.Localization;

public class LocalizationMiddleware
{
    private readonly RequestDelegate _next;

    public LocalizationMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException("next");
    }

    public async Task Invoke(HttpContext context, ILocalizationService localizationService)
    {
        var acceptLanguage = context.Request.GetTypedHeaders().AcceptLanguage;
        if (acceptLanguage.Count > 0)
        {
            localizationService.AcceptLocales = (from x in acceptLanguage
                                                 orderby x.Quality.GetValueOrDefault(1.0) descending
                                                 select x.Value.ToString()).ToImmutableArray();
        }
        else if (context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
        {
            var queryAcceptLanguage = context.Request.Query["Accept-Language"];
            if (!string.IsNullOrEmpty(queryAcceptLanguage))
            {
                localizationService.AcceptLocales = queryAcceptLanguage.ToString().Split(',').ToImmutableArray();
            }
        }

        await _next(context);
    }
}