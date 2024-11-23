using Kata.Challange.Characters.Services.Providers;

namespace Kata.Challange.Characters.Services.Services
{
    /// <summary>
    /// Write characters to destination using source provider
    /// </summary>
    public interface IDestinationService
    {
        /// <summary>
        /// Writes a single character to a destination
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        Task WriteCharAsync(char character);

        /// <summary>
        /// Writes a multiple characters to a destination
        /// </summary>
        /// <param name="characters"></param>
        /// <returns></returns>
        Task WriteCharsAsync(IEnumerable<char> characters);
    }
    public class DestinationService(ICharacterProvider characterProvider) : IDestinationService
    {
        public async Task WriteCharAsync(char character)
        {
            await characterProvider.AddCharacterAsync(character);
        }

        public async Task WriteCharsAsync(IEnumerable<char> characters)
        {
            await characterProvider.AddCharactersAsync(characters);
        }
    }
}
