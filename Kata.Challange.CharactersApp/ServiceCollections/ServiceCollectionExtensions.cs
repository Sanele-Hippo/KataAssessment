using Kata.Challange.Characters.Services.Providers;
using Kata.Challange.Characters.Services.Services;
using Microsoft.Extensions.DependencyInjection; 

namespace Kata.Challange.CharactersApp.ServiceCollections
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceCollection AddServices(this ServiceCollection servicesCollection)
        {
            servicesCollection.AddScoped<ICharacterProvider>(sp => new CharacterProvider([]));
            servicesCollection.AddScoped<ISourceService, SourceService>();
            servicesCollection.AddScoped<IDestinationService, DestinationService>();
            servicesCollection.AddScoped<Copier>();
            return servicesCollection;
        }
    }
}
