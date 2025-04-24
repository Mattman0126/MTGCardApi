using MTGCardApi.Data;
using MTGCardApi.Models.Cards;
using MTGCardApi.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<IMagicCardService, MagicCardService>();
        services.AddHttpClient<IScryfallService, ScryfallService>();

        return services;
    }
}