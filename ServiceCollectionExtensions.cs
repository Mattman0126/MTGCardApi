using MTGCardApi.Data;
using MTGCardApi.Data.Repository;
using MTGCardApi.Models.Cards;
using MTGCardApi.Models.Decks;
using MTGCardApi.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //repositories
        services.AddScoped<ICardRepository, CardRepository>();        
        services.AddScoped<IDeckRepository, DeckRepository>();

        //services
        services.AddScoped<IMagicDeckService, MagicDeckService>();
        services.AddScoped<IMagicCardService, MagicCardService>();
        services.AddHttpClient<IScryfallService, ScryfallService>(client => { 
            client.DefaultRequestHeaders.UserAgent.ParseAdd("MTGCardApi/1.0");
            client.Timeout = TimeSpan.FromMinutes(10);
        });

        return services;
    }
}