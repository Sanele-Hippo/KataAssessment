using Kata.Challange.Characters.Services.Providers;

namespace Kata.Challange.Characters.Services.Services
{
    /// <summary>
    /// Copy characters from the source provider
    /// </summary>
    public interface ISourceService
    {
        /// <summary>
        /// Read a single character from the source
        /// </summary>
        /// <returns></returns>
        IAsyncEnumerable<char> ReadCharAsync();

        /// <summary>
        /// Reads a multiple characters from the source
        /// </summary>
        /// <param name="count"></param>
        /// <returns>IEnumerable<char></returns>
        Task<IEnumerable<char>> ReadCharsAsync(int count);
    }

    public class SourceService(ICharacterProvider characterProvider) : ISourceService
    {
        public async IAsyncEnumerable<char> ReadCharAsync()
        {
            var characters = characterProvider.GetCharacterAsync();
            await foreach (var character in characters)
                yield return character;
        }

        public async Task<IEnumerable<char>> ReadCharsAsync(int count)
        {
            var characters = await characterProvider.GetCharactersAsync(count);
            return characters;
        }
    }
}
