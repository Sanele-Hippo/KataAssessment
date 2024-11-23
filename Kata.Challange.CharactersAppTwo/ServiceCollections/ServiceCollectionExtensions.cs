using Kata.Challange.Characters.Services.Providers;
using Kata.Challange.Characters.Services.Services;
using Kata.Challange.CharactersAppTwo.Enums;
using Kata.Challange.CharactersAppTwo.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace Kata.Challange.CharactersAppTwo.ServiceCollections
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceCollection AddServices(this ServiceCollection servicesCollection)
        {
            servicesCollection.AddScoped<ICharacterProvider>(sp => new CharacterProvider([]));
            servicesCollection.AddScoped<ISourceService, SourceService>();
            servicesCollection.AddScoped<IDestinationService, DestinationService>();
            servicesCollection.AddScoped(sp => new Copier(GetCopyProcesses(sp)));
            return servicesCollection;
        }

        public static Dictionary<CopyMechanism, Func<ICopyStrategy>> GetCopyProcesses(IServiceProvider serviceProvider)
        {
            return new Dictionary<CopyMechanism, Func<ICopyStrategy>>
            {
                {
                    CopyMechanism.Single,
                    () => new CopySingleStrategy(serviceProvider.GetRequiredService<ISourceService>(),
                        serviceProvider.GetRequiredService<IDestinationService>())
                },
                {
                    CopyMechanism.Multiple,
                    () => new CopyMultipleStrategy(serviceProvider.GetRequiredService<ISourceService>(),
                        serviceProvider.GetRequiredService<IDestinationService>())
                }
            };
        }
    }
}
