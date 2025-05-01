using MTGCardApi.Data;
using MTGCardApi.Models.Cards;
using MTGCardApi.Services;
using System.Net.Http;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<IMagicCardService, MagicCardService>();
        services.AddHttpClient<IScryfallService, ScryfallService>(client => { 
            client.DefaultRequestHeaders.UserAgent.ParseAdd("MTGCardApi/1.0");
            client.Timeout = TimeSpan.FromMinutes(10);
        });

        return services;
    }
}