using Kata.Challange.Characters.Services.Services;

namespace Kata.Challange.CharactersAppTwo.Strategies
{
    /// <summary>
    /// Copy single responsable for copying one character at a time from the source to destination
    /// </summary>
    public class CopySingleStrategy(ISourceService sourceService,
        IDestinationService destination) : ICopyStrategy
    {
        public async Task Execute()
        {
            await foreach (var character in sourceService.ReadCharAsync())
            {
                if (character.Equals('\n'))
                    break;
                
                await destination.WriteCharAsync(character);
            }
        }
    }
}
