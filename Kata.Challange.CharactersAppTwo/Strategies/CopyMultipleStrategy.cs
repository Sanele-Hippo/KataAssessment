using Kata.Challange.Characters.Services.Services;
namespace Kata.Challange.CharactersAppTwo.Strategies
{
    /// <summary>
    /// Copy multiple responsable for copying number of speficied characters from the source to destination
    /// </summary>
    public class CopyMultipleStrategy(ISourceService sourceService, 
        IDestinationService destinationService) : ICopyStrategy
    {
        private const int _count = 6;
        private const char _newLine = '\n';
        public async Task Execute()
        {
            var characters = await sourceService.ReadCharsAsync(_count);
            var charsToCopy = characters.TakeWhile(character => !character.Equals(_newLine));

            if (charsToCopy.Any())
            {
                await destinationService.WriteCharsAsync(charsToCopy);
            }
        }
    }
}
